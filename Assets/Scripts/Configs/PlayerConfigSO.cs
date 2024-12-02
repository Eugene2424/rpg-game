using UnityEngine;

namespace Game.Configs
{
    public class PlayerConfigSO : ScriptableObject, IPlayerConfig
    {
        [SerializeField] private int maxHp;
        [SerializeField] private int maxMp;
        [SerializeField] private int def;
        [SerializeField] private int str;
        [SerializeField] private int mag;
        [SerializeField] private int exp;
        [SerializeField] private int level;
        [SerializeField] private int hpPotions;
        [SerializeField] private int mpPotions;
        [SerializeField] private int strPotions;
        [SerializeField] private PlayerType playerType;
        
        public int MaxHp => maxHp;
        public int MaxMp => maxMp;
        public int Def => def;
        public int Str => str; 
        public int Mag => mag;
        public int Exp => exp;
        public int Level => level;
        public int InitialHpPotions => hpPotions;
        public int InitialMpPotions => mpPotions;
        public int InitialStrPotions => strPotions;
        public PlayerType Type => playerType;
    }
}