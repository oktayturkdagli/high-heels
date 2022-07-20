using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[ExecuteInEditMode]
public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelContainer levelContainer;
    
    public LevelContainer LevelContainer { get => levelContainer; set => levelContainer = value; }
    public static LevelManager Instance { get; private set; }
    
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
    
    private void OnEnable()
    {
        Load();
        if (levelContainer == null || levelContainer.levels == null || levelContainer.levels.Count < 1)
        {
            levelContainer = new LevelContainer();
            levelContainer.levels = new List<Level>();
            levelContainer.levels.Add(new Level());
            Save();
        }
    }
    
    private void Load()
    {
        FileManager.Instance.ReadLevelData();
    }
    
    public void Save()
    {
        FileManager.Instance.WriteLevelData(levelContainer);
    }
    
    public Level BringLevel(int levelIndex)
    {
        if (levelContainer == null || levelContainer.levels == null)
            return null;

        if (levelIndex > levelContainer.levels.Count)
        {
            CreateLevel(levelIndex - levelContainer.levels.Count);
        }
        
        return levelContainer.levels[levelIndex - 1];
    }

    private void CreateLevel(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            levelContainer.levels.Add(new Level());
        }
    }
    
    private void DrawLevel(int levelIndex)
    {
        if (levelIndex - 1 > levelContainer.levels.Count - 1)
            levelIndex = Random.Range(1, levelContainer.levels.Count - 1);

        Level level = levelContainer.levels[levelIndex];
        List<LevelItem> levelGrid = level.levelGrid;
        levelGrid = levelGrid.OrderBy(x => x.position.x).ThenBy(x => x.position.z).ToList();
        for (var i = 0; i < levelGrid.Count; i++)
        {
            GameObject item = ObjectPool.Instance.GetPooledObject(levelGrid[i].type, levelGrid[i].position, Vector3.zero);
            if (levelGrid[i].type == ItemTypes.Road)
            {
                item.transform.localScale = new Vector3(level.width, 1, level.height);
                var position = item.transform.position;
                position = new Vector3(position.x, -1, position.z);
                item.transform.position = position;
            }
            else if (levelGrid[i].type == ItemTypes.Finish)
            {
                item.transform.localScale = new Vector3(level.width * 2, 1, level.height * 2);
                var position = item.transform.position;
                position = new Vector3(position.x, -1, position.z);
                item.transform.position = position;
            }
        }
    }

}
