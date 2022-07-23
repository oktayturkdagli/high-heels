using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    [System.Serializable]
    public class LevelDataContainer
    {
        public List<LevelData> levelDataList;
    }
    
    [System.Serializable]
    public class LevelData
    {
        public List<LevelItem> levelGrid = new List<LevelItem>();
        public int width = 5;
        public int height = 150;
    }
    
    [System.Serializable]
    public class LevelItem
    {
        public EnvironmentalItemTypes type;
        public Vector3 position;
    }
}