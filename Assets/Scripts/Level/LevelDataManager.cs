using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Game
{
    [ExecuteInEditMode]
    public class LevelDataManager : MonoBehaviour
    {
        [SerializeField] private LevelDataContainer levelDataContainer;
        
        public LevelDataContainer LevelDataContainer { get => levelDataContainer; set => levelDataContainer = value; }
        public static LevelDataManager Instance { get; set; }

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

            LoadInitialValues();
        }

        private void LoadInitialValues()
        {
            LoadData();
            if (levelDataContainer == null || levelDataContainer.levelDataList == null || levelDataContainer.levelDataList.Count < 1)
            {
                levelDataContainer = new LevelDataContainer();
                levelDataContainer.levelDataList = new List<LevelData>();
                levelDataContainer.levelDataList.Add(new LevelData());
                SaveData();
            } 
        }

        private void LoadData()
        {
            FileManager.Instance.ReadData(this, DataType.Level);
        }

        public void SaveData()
        {
            FileManager.Instance.WriteData(this, DataType.Level);
            LoadData();
        }
        
        public void DrawLevel(int levelIndex)
        {
            LoadData();
            
            if (levelIndex > levelDataContainer.levelDataList.Count)
            {
                levelIndex = (levelIndex % levelDataContainer.levelDataList.Count) + 1;
            }
                
            
            Level level = new Level(levelDataContainer.levelDataList[levelIndex - 1]);
            level.Draw();
        }
        
        public void NextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
