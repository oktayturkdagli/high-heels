using UnityEngine;
using System.IO;

namespace Game
{
    [ExecuteInEditMode]
    public class FileManager : MonoBehaviour
    {
        [SerializeField] private TextAsset levelDataFile;
        [SerializeField] private TextAsset playerDataFile;

        public static FileManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        public void ReadLevelData(LevelManager levelManager)
        {
            if (levelDataFile != null)
            {
                levelManager.LevelDataContainer = ReadFile<LevelDataContainer>(levelDataFile);
            }
        }

        public void WriteLevelData(LevelDataContainer objectType)
        {
            if (levelDataFile != null && objectType != null)
            {
                WriteFile(objectType, levelDataFile);
            }
        }
        
        public void ReadPlayerData(PlayerManager playerManager)
        {
            if (playerDataFile != null)
            { 
               playerManager.PlayerDataContainer = ReadFile<PlayerDataContainer>(playerDataFile);
            }
        }
        
        public void WritePlayerData(PlayerDataContainer objectType)
        {
            if (playerDataFile != null)
            {
                WriteFile(objectType, playerDataFile);
            }
        }

        private T ReadFile<T>(TextAsset fileToRead)
        {
            return JsonUtility.FromJson<T>(fileToRead.text);
        }

        private void WriteFile(object dataType, TextAsset file)
        {
            string data = JsonUtility.ToJson(dataType);
            File.WriteAllText(Application.dataPath + "/Resources/Jsons/" + file.name + ".json", data);
        }
        
    }
}