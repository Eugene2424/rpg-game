using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance;

    public BattleWindow battleWindow;
    public Dictionary<string, Vector3> savedPos = new Dictionary<string, Vector3>();

    public string battleEnemyName, lastScene, battleSceneName;
    public int levelForBattle;

    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(Instance);

        savedPos["Forest2"] = Vector3.zero;
        savedPos["Cave1"] = Vector3.zero;
    }

}
