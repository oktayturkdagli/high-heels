using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
            LoadLevelData();
            if (levelDataContainer == null || levelDataContainer.levelDataList == null || levelDataContainer.levelDataList.Count < 1)
            {
                levelDataContainer = new LevelDataContainer();
                levelDataContainer.levelDataList = new List<LevelData>();
                levelDataContainer.levelDataList.Add(new LevelData());
                SaveLevelData();
            } 
        }

        private void LoadLevelData()
        {
            FileManager.Instance.ReadData(this, DataType.Level);
        }

        public void SaveLevelData()
        {
            FileManager.Instance.WriteData(this, DataType.Level);
            LoadLevelData();
        }
        
        public void DrawLevel(int levelIndex)
        {
            LoadLevelData();
            
            if (levelIndex > levelDataContainer.levelDataList.Count)
                levelIndex = 1;
            
            Level level = new Level(levelDataContainer.levelDataList[levelIndex - 1]);
            level.Draw();
        }
        
    }
}
