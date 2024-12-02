using UnityEngine;
using System.IO;

namespace Game.Services
{
    public class JsonPlayerDataRepository : IPlayerDataRepository
    {
        private readonly string _filePath = Path.Combine(Application.persistentDataPath, "/gameData.json");
        
        public void SaveData(PlayerData data)
        {
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(_filePath, json);
        }

        public PlayerData LoadData()
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                return JsonUtility.FromJson<PlayerData>(json);
            }

            return null;
        }

    }
}