using UnityEngine;
using System.IO;

namespace Game
{
    [ExecuteInEditMode]
    public class FileManager : MonoBehaviour
    {
        [SerializeField] private TextAsset levelData;
        [SerializeField] private TextAsset playerData;

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

        public void ReadLevelData()
        {
            if (levelData != null)
            {
                LevelManager.Instance.LevelDataContainer = ReadFile<LevelDataContainer>(levelData);
            }
        }

        public void WriteLevelData(LevelDataContainer objectType)
        {
            if (levelData != null && objectType != null)
            {
                WriteFile(objectType, levelData);
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