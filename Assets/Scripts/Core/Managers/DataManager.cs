using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Localization;
using Game;


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
        if (data.Exp >= _baseData.Exp * (int)Mathf.Pow(data.Level + 1, 1.6f))
        {
            data.Level++;
            if (data.type == PlayerType.Warrior)
            {
                data.MaxHp = _baseData.MaxHp + data.Level * 25;
                data.MaxMp = _baseData.MaxHp + data.Level * 5;
                data.Def = _baseData.Def + data.Level * 10;
                data.Str = _baseData.Str + data.Level * 10;
                data.Mag = _baseData.Mag + data.Level * 12;
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
        data.Exp = data.Exp + ExpValueFromEnemy(enemy);
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
