using UnityEngine;
using System.IO;

namespace Game
{
    [ExecuteInEditMode]
    public class FileManager : MonoBehaviour
    {
        [SerializeField] private TextAsset levelDataFile;
        [SerializeField] private TextAsset playerDataFile;
        private bool isLevelDataFileNull, isPlayerDataFileNull;

        public static FileManager Instance { get; set; }

        private void Start()
        {
            isLevelDataFileNull = levelDataFile == null;
            isPlayerDataFileNull = playerDataFile == null;
        }

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
            if (isLevelDataFileNull) return;
            string data = File.ReadAllText(Application.dataPath + "/Resources/Jsons/" + levelDataFile.name + ".json");
            levelManager.LevelDataContainer = JsonUtility.FromJson<LevelDataContainer>(data);
        }

        public void WriteLevelData(LevelDataContainer objectType)
        {
            if (isLevelDataFileNull || objectType == null)
                return; 
            
            WriteFile(objectType, levelDataFile);
            
        }
        
        public void ReadPlayerData(PlayerDataManager playerDataManager)
        {
            if (isPlayerDataFileNull) return;
            string data = File.ReadAllText(Application.dataPath + "/Resources/Jsons/" + playerDataFile.name + ".json");
            playerDataManager.PlayerDataContainer = JsonUtility.FromJson<PlayerDataContainer>(data);
        }
        
        public void WritePlayerData(PlayerDataContainer objectType)
        {
            if (isPlayerDataFileNull || objectType == null)
                return; 
            
            WriteFile(objectType, playerDataFile);
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