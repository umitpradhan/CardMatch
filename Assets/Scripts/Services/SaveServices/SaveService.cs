using System.IO;
using UnityEngine;

namespace CardMatch.Services.SaveServices
{
    public sealed class SaveService
    {
        private readonly string _filePath;

        public SaveService()
        {
            _filePath = Path.Combine(
                Application.persistentDataPath,
                "game_save.json");
        }

        public void Save(GameSaveData data)
        {
            string json = JsonUtility.ToJson(data, true);

            File.WriteAllText(_filePath, json);
        }

        public GameSaveData Load()
        {
            if (!File.Exists(_filePath))
                return null;

            string json = File.ReadAllText(_filePath);

            return JsonUtility.FromJson<GameSaveData>(json);
        }

        public bool HasSave()
        {
            return File.Exists(_filePath);
        }

        public void Delete()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);
        }
    }
}