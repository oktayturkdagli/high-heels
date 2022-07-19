using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(menuName = "Scriptable Objects/Level", fileName = "New Level")]
public class SOLevel : ScriptableObject
{
    [SerializeField] private List<LevelItem> levelGrid = new List<LevelItem>();
    [SerializeField] private int width = 5;
    [SerializeField] private int height = 5;
    
    public List<LevelItem> LevelGrid { get => levelGrid; set => levelGrid = value; }
    public int Width { get => width; set => width = value; }
    public int Height { get => height; set => height = value; }
    
    public void CreateLevel()
    {
        levelGrid = levelGrid.OrderBy(x => x.position.x).ThenBy(x => x.position.z).ToList();
        for (int i = 0; i < levelGrid.Count; i++)
        {
            GameObject item = ObjectPool.SharedInstance.GetPooledObject(levelGrid[i].type, levelGrid[i].position, Vector3.zero);
            if (levelGrid[i].type == ItemTypes.Road)
            {
                item.transform.localScale = new Vector3(width, 1, height);
                item.transform.position = new Vector3(item.transform.position.x, -1, item.transform.position.z);
            }
            else if (levelGrid[i].type == ItemTypes.Finish)
            {
                item.transform.localScale = new Vector3(width * 2, 1, width * 2);
                item.transform.position = new Vector3(item.transform.position.x, -1, item.transform.position.z);
            }
                
        }
    }
    
}