using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Localization;

public enum PlayerType
{
    MAGE,
    WARRIOR
}


[Serializable]
public class PlayerData
{
    public int maxHp = 140, maxMp = 40, def = 20, str = 22, mag = 30, exp = 1000, level = 1;
    public int hpPotions = 1, mpPotions = 1, strPotions = 1;
    public PlayerType type = PlayerType.WARRIOR;

    public int langId = -1;
}

public class DataManager : MonoBehaviour
{
    public PlayerData data = new PlayerData();
    private PlayerData _baseData = new PlayerData();
    public static DataManager Instance;


    private void Awake()
    { 
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (GetData() == null)
        {
            data = _baseData;
            SaveData();
        }

        else
        {
            data = GetData();
        }
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/gameData.json", json);
        data = GetData();
    }

    private PlayerData GetData()
    {
        if (File.Exists(Application.persistentDataPath + "/gameData.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/gameData.json");
            return JsonUtility.FromJson<PlayerData>(json);
        }
        return null;
    }

    public void CheckExpToLevelUp()
    {
        if (data.exp >= _baseData.exp * (int)Mathf.Pow(data.level + 1, 1.6f))
        {
            data.level++;
            if (data.type == PlayerType.WARRIOR)
            {
                data.maxHp = _baseData.maxHp + data.level * 25;
                data.maxMp = _baseData.maxHp + data.level * 5;
                data.def = _baseData.def + data.level * 10;
                data.str = _baseData.str + data.level * 10;
                data.mag = _baseData.mag + data.level * 12;
            }
            SaveData();
            data = GetData();
        } 
    }

    public void EraseData()
    {
        data = _baseData;
        SaveData();
    }

    public int ExpValueFromEnemy(Enemy enemy) => 10 * ((enemy.Atk + enemy.GetMaxHp()) / 2);

    public void GainExpFromEnemy(Enemy enemy)
    {
        data.exp = data.exp + ExpValueFromEnemy(enemy);
        SaveData();
    }

    public void UsedPotion(string potionType)
    {
        if (potionType == "HP")
            data.hpPotions--;
        else if (potionType == "MP")
            data.mpPotions--;
        else if (potionType == "STR")
            data.strPotions--;
        SaveData();
    }
}
