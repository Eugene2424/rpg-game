using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class BattleView : MonoBehaviour
{
    public event UnityAction FireButtonClicked, 
                             ShieldButtonClicked, 
                             HealButtonClicked, 
                             AttackButtonClicked, 
                             EscapeButtonClicked;

    [Header("Result Screens")]
    [SerializeField] private GameObject failureScreen;
    [SerializeField] private GameObject victoryScreen;
    
    [Header("Sound Effects")]
    [SerializeField] private AudioClip victorySfx;
    [SerializeField] private AudioClip failureSfx;
    
    [Header("UI")]
    [SerializeField] private Button attackBtn;
    [SerializeField] private Button escapeBtn;
    [SerializeField] private Button fireBtn;
    [SerializeField] private Button shieldBtn;
    [SerializeField] private Button healBtn;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private TextMeshProUGUI hpPotionText, mpPotionText, strPotionText;

    private AudioSource audioManager;

    private void Start()
    {
        audioManager = GetComponent<AudioSource>();
        
        attackBtn.onClick.AddListener(AttackButtonClicked);
        escapeBtn.onClick.AddListener(EscapeButtonClicked);
        fireBtn.onClick.AddListener(FireButtonClicked);
        shieldBtn.onClick.AddListener(ShieldButtonClicked);
        healBtn.onClick.AddListener(HealButtonClicked);
    }

    private void Show(GameObject obj)
    {
        obj.SetActive(true);
    }

    private void Hide(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void CheckLevelUpdateUI()
    {
        DataManager.Instance.CheckExpToLevelUp();
        if (DataManager.Instance.data.Level >= 1)
            Show(healBtn.gameObject);
        if (DataManager.Instance.data.Level >= 4)
            Show(shieldBtn.gameObject);
        if (DataManager.Instance.data.Level >= 4)
            Show(fireBtn.gameObject);

        hpPotionText.text = DataManager.Instance.data.hpPotions.ToString();
        mpPotionText.text = DataManager.Instance.data.mpPotions.ToString();
        strPotionText.text = DataManager.Instance.data.strPotions.ToString();
    }

    private void ShowLoseScreen()
    {
        Show(failureScreen);

        audioManager.Stop();
        audioManager.clip = failureSfx;
        audioManager.volume = 1;
        audioManager.loop = false;
        audioManager.Play();
    }

    private void ShowWinScreen()
    {
        Show(victoryScreen);

        audioManager.Stop();
        audioManager.clip = victorySfx;
        audioManager.volume = 1;
        audioManager.loop = false;
        audioManager.Play();
    }

    private void ShowEnemies(Enemy[] enemies)
    {
        foreach (Enemy enemy in enemies)
        {
            if (enemy.enemyName == WorldManager.Instance.battleEnemyName)
            {
                Show(enemy.transform.gameObject);
            }
        }
    }
}
