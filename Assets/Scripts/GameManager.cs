using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager SharedInstance;
    [SerializeField] public SOLevelsData levelsData;
    private SOLevel level;
    private List<LevelItem> levelGrid;
    
    public List<LevelItem> LevelGrid { get => levelGrid; set => levelGrid = value; }
    
    private void Awake()
    {
        SharedInstance = this;
    }
    
    private void OnEnable()
    {
        EventManager.SharedInstance.onStartGame += OnStartGame;
        EventManager.SharedInstance.onFinishGame += OnFinishGame;
    }
    
    private void OnDisable()
    {
        EventManager.SharedInstance.onStartGame -= OnStartGame;
        EventManager.SharedInstance.onFinishGame -= OnFinishGame;
    }
    
    private void Start()
    {
        level = levelsData.GetLevel();
        levelGrid = level.LevelGrid;
        levelsData.DrawLevel();
        EventManager.SharedInstance.OnStartGame();
    }
    
    private void OnStartGame()
    {
        
    }
    
    private void OnFinishGame()
    {
        //Do Nothing
    }
    

    public void OnDown()
    {
        
    }

}
