namespace Game.Interfaces
{
    public interface IPlayerDataService
    {
        public void SaveData();
        public PlayerData GetData();
    }
}