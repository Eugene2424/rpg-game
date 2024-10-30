using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform _spawnAfterBattle;
    [SerializeField] private int _levelForBattle = 1;
    public string enemyName, sceneName;

    public void PutDataForBattle()
    {
        WorldManager.Instance.battleEnemyName = enemyName;
        WorldManager.Instance.battleSceneName = sceneName;
        WorldManager.Instance.levelForBattle = _levelForBattle;
        WorldManager.Instance.lastScene = SceneManager.GetActiveScene().name;
        WorldManager.Instance.savedPos[SceneManager.GetActiveScene().name] = _spawnAfterBattle.position;
    }
}
