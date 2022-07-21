using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    [System.Serializable]
    public class Level
    {
        public List<LevelItem> levelGrid = new List<LevelItem>();
        public int width = 5;
        public int height = 150;
    }

    [System.Serializable]
    public class LevelContainer
    {
        public List<Level> levels;
    }

    [System.Serializable]
    public class LevelItem
    {
        public ItemTypes type;
        public Vector3 position;
    }
}