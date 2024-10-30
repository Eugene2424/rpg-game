using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [Header("Result Screens")]
    [SerializeField] private GameObject failureScreen;
    [SerializeField] private GameObject victoryScreen;
    [Header("Sound Effects")]
    [SerializeField] private AudioClip victorySfx;
    [SerializeField] private AudioClip failureSfx;
    [Header("UI")]
    [SerializeField] private GameObject fireButton;
    [SerializeField] private GameObject shieldButton;
    [SerializeField] private GameObject healButton;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private TextMeshProUGUI hpPotionText, mpPotionText, strPotionText;


    private bool isActionCompleted = false, isBattleGoing = true;
    private AudioSource audioManager;

    private Enemy[] _enemies;
    private Player _player;

    private void Start()
    {
        audioManager = GetComponent<AudioSource>();

        _enemies = FindObjectsOfType<Enemy>(true);
        _player = FindObjectOfType<Player>();

        ShowEnemies();
    }


    private void Update()
    {
        if (isBattleGoing)
        {
            CheckLevelUpdateUI();
            if (AreEnemiesDead() && !_player.IsDead)
                Win();
            else if (!AreEnemiesDead() && _player.IsDead)
                Lose();
            else
            {
                if (isActionCompleted && !(AreEnemiesDead()))
                {
                    StartCoroutine(ActionEnemy());
                }
                gameUI.SetActive(_player.CanTakeAction);
            }
        }
    }

    IEnumerator ActionEnemy()
    {
        isActionCompleted = false;
        
        foreach (Enemy enemy in _enemies)
        {
            if (enemy.gameObject.activeInHierarchy && !enemy.IsDead)
            {
                yield return new WaitForSeconds(3);
                enemy.Action(_player);
            }
            
        }
        
        yield return new WaitForSeconds(3);
        _player.CanTakeAction = true;

    }

    public void AttackEnemy()
    {
        if (GetSelectedEnemy() != null)
        {
            _player.Action(_player.AttackRoutine(GetSelectedEnemy()));
            isActionCompleted = true;
        }
    }

    public void FireAttack()
    {
        if (GetSelectedEnemy() != null)
        {
            _player.Action(_player.FireRoutine(GetSelectedEnemy()));
            isActionCompleted = true;
        } 
    }

    public void ShieldMagic()
    {
        _player.Action(_player.ShieldRoutine());
        isActionCompleted = true;
    }

    public void HealPlayer()
    {
        _player.Action(_player.HealRoutine());
        isActionCompleted = true;
    }

    public void UsePotion(string potion)
    {
        _player.Action(_player.UsePotionRoutine(potion));
        isActionCompleted = true;
    }

    public void Show(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void Hide(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Escape()
    {
        StartCoroutine(EscapeRoutine());
    }

    private void CheckLevelUpdateUI()
    {
        DataManager.Instance.CheckExpToLevelUp();
        if (DataManager.Instance.data.level >= 1)
            Show(healButton);
        if (DataManager.Instance.data.level >= 4)
            Show(shieldButton);
        if (DataManager.Instance.data.level >= 4)
            Show(fireButton);

        hpPotionText.text = DataManager.Instance.data.hpPotions.ToString();
        mpPotionText.text = DataManager.Instance.data.mpPotions.ToString();
        strPotionText.text = DataManager.Instance.data.strPotions.ToString();
    }
    private Enemy GetSelectedEnemy()
    {
        foreach (Enemy enemy in _enemies)
        {
            if (enemy.IsSelectedByPlayer)
                return enemy;
        }
        return null;
    }

    private bool AreEnemiesDead()
    {
        int count = 0;
        foreach (var enemy in _enemies)
        {
            if (!enemy.gameObject.activeInHierarchy)
                count++;
        }
        return _enemies.Length == count;
    }

    private void ShowEnemies()
    {

        foreach (Enemy enemy in _enemies)
        {
            if (enemy.enemyName == WorldManager.Instance.battleEnemyName)
            {
                enemy.transform.gameObject.SetActive(true);
            }
        }
    }

    private void Lose()
    {
        StartCoroutine(LoseRoutine());
    }

    private void Win()
    {
        StartCoroutine(WinRoutine());
    }

    IEnumerator EscapeRoutine()
    {
        isBattleGoing = false;
        failureScreen.SetActive(true);

        audioManager.Stop();
        audioManager.clip = failureSfx;
        audioManager.volume = 1;
        audioManager.loop = false;
        audioManager.Play();

        yield return new WaitForSeconds(6);
        SceneManager.LoadScene(WorldManager.Instance.lastScene);
    }

    IEnumerator LoseRoutine()
    {
        isBattleGoing = false;
        failureScreen.SetActive(true);

        audioManager.Stop();
        audioManager.clip = failureSfx;
        audioManager.volume = 1;
        audioManager.loop = false;
        audioManager.Play();

        DataManager.Instance.EraseData();
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene(WorldManager.Instance.lastScene);
    }

    IEnumerator WinRoutine()
    {
        isBattleGoing = false;

        victoryScreen.SetActive(true);
        victoryScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "EXP +" + (GainExpFromEnemies()).ToString();

        audioManager.Stop();
        audioManager.clip = victorySfx;
        audioManager.volume = 1;
        audioManager.loop = false;
        audioManager.Play();

        yield return new WaitForSeconds(6);
        SceneManager.LoadScene(WorldManager.Instance.lastScene);
    }

    private int GainExpFromEnemies()
    {
        int totalExp = 0;
        foreach (Enemy enemy in _enemies)
        {
            if (enemy.IsDead)
            {
                DataManager.Instance.GainExpFromEnemy(enemy);
                totalExp += DataManager.Instance.ExpValueFromEnemy(enemy);
            }

        }
        return totalExp;
    }
}
