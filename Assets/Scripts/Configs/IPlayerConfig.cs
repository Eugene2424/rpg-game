namespace Game.Configs
{
    public interface IPlayerConfig
    {
        public int MaxHp { get; }
        public int MaxMp { get; }
        public int Def { get; }
        public int Str { get; } 
        public int Mag { get; } 
        public int Exp { get; } 
        public int Level { get; }
        public int InitialHpPotions { get; }
        public int InitialMpPotions { get; }
        public int InitialStrPotions { get; }
        
        public PlayerType Type { get; }
    }
}