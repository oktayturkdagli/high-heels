using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class Level
    {
        private LevelData levelData;
        
        public LevelData LevelData { get => levelData; set => levelData = value; }
        
        public Level(LevelData levelData)
        {
            this.levelData = levelData;
        }

        public void Draw()
        {
            List<LevelItem> levelGrid = levelData.levelGrid;
            levelGrid = levelGrid.OrderBy(x => x.position.x).ThenBy(x => x.position.z).ToList();
            for (var i = 0; i < levelGrid.Count; i++)
            {
                GameObject item =
                    ObjectPool.Instance.GetPooledObject(levelGrid[i].type, levelGrid[i].position, Vector3.zero);
                if (levelGrid[i].type == EnvironmentalItemTypes.Road)
                {
                    item.transform.localScale = new Vector3(levelData.width, 1, levelData.height);
                    var position = item.transform.position;
                    position = new Vector3(position.x, -1, position.z);
                    item.transform.position = position;
                }
                else if (levelGrid[i].type == EnvironmentalItemTypes.Finish)
                {
                    item.transform.localScale = new Vector3(levelData.width * 2, 1, levelData.height * 2);
                    var position = item.transform.position;
                    position = new Vector3(position.x, -1, position.z);
                    item.transform.position = position;
                }
            }
        }
        
    }
}