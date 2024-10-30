using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _enemyNameText, _levelForBattleText;

    private void Start()
    {
        WorldManager.Instance.battleWindow = this;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _enemyNameText.text = WorldManager.Instance.battleEnemyName;
        _levelForBattleText.text = "Recommended level: " + WorldManager.Instance.levelForBattle.ToString();
    }

    public void StartBattle()
    {
        SceneManager.LoadScene(WorldManager.Instance.battleSceneName);
    }

    public void CancelBattle()
    {
        WorldManager.Instance.savedPos[SceneManager.GetActiveScene().name] = Vector3.zero;
    }
}
