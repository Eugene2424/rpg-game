namespace Game.Services
{
    public interface IPlayerDataRepository
    {
        public void SaveData(PlayerData data);
        public PlayerData LoadData();
    }
}