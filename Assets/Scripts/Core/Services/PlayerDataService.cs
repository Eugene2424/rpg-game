using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.IO;
using Game.Configs;
using Game.Interfaces;


namespace Game
{
    public enum PlayerType
    {
        Mage,
        Warrior
    }


    public enum PotionType
    {
        HP,
        MP,
        STR
    }

    public class PlayerData
    {
        public int MaxHp { get; private set; }
        public int MaxMp { get; private set; }
        public int Def { get; private set; }
        public int Str { get; private set; }
        public int Mag { get; private set; }
        public int Exp { get; private set; }
        public int Level { get; private set; }

        public int HpPotions { get; private set; }
        public int MpPotions { get; private set; }
        public int StrPotions { get; private set; }
        public PlayerType Type { get; private set; }

        

        public void Initialize(IPlayerConfig config)
        {
            Type = config.Type;
            Level = config.Level;
            Exp = config.Exp;

            MaxHp = config.MaxHp;
            MaxMp = config.MaxMp;
            Def = config.Def;
            Str = config.Str;
            Mag = config.Mag;

            HpPotions = config.InitialHpPotions;
            MpPotions = config.InitialMpPotions;
            StrPotions = config.InitialStrPotions;
        }

        public bool CheckLevelUp(int expThreshold)
        {
            if (Exp >= expThreshold)
            {
                Level++;
                UpdateStats();
                return true;
            }
            return false;
        }

        private void UpdateStats()
        {
            MaxHp += 25 * Level;
            MaxMp += 5 * Level;
            Def += 10 * Level;
            Str += 10 * Level;
            Mag += 12 * Level;
        }

        public void UsePotion(PotionType potionType)
        {
            switch (potionType)
            {
                case PotionType.HP:
                    HpPotions--;
                    break;
                case PotionType.MP:
                    MpPotions--;
                    break;
                case PotionType.STR:
                    StrPotions--;
                    break;
            }
        }

        public void GainExp(int amount)
        {
            Exp += amount;
        }
    }
}


namespace Game.Services
{
    
    public class PlayerDataService : IPlayerDataService
    {
        
        private readonly IPlayerDataRepository _dataRepository;
        private readonly IPlayerConfig _config;
        
        private PlayerData _data;

        public PlayerDataService(IPlayerDataRepository dataRepository, IPlayerConfig config)
        {
            _dataRepository = dataRepository;
            _config = config;
        }
        
        public void SaveData()
        {
            _dataRepository.SaveData(_data);
        }

        public PlayerData GetData()
        {
            if (_data == null)
            {
                _data = _dataRepository.LoadData() ?? InitializeDefaultData();
            }
            return _data;
        }

        private PlayerData InitializeDefaultData()
        {
            var newData = new PlayerData();
            newData.Initialize(_config); // Example default type
            SaveData();
            return newData;
        }

        public void CheckExpToLevelUp()
        {
            var expThreshold = _config.Exp * (int)Mathf.Pow(_data.Level + 1, 1.6f);
            if (_data.CheckLevelUp(expThreshold))
            {
                SaveData();
            }
        }

        public void GainExpFromEnemy(Enemy enemy)
        {
            var expGained = 10 * ((enemy.Atk + enemy.GetMaxHp()) / 2);
            _data.GainExp(expGained);
            SaveData();
        }

        public void UsePotion(PotionType potionType)
        {
            _data.UsePotion(potionType);
            SaveData();
        }
    
    }

}
