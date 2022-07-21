using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int level = 1;
    
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
        EventManager.Instance.onStartGame += OnStartGame;
        EventManager.Instance.onFinishGame += OnFinishGame;
    }
    
    private void OnDisable()
    {
        EventManager.Instance.onStartGame -= OnStartGame;
        EventManager.Instance.onFinishGame -= OnFinishGame;
    }
    
    private void Start()
    {
        EventManager.Instance.OnStartGame();
        LevelManager.Instance.DrawLevel(level);
    }
    
    private void OnStartGame()
    {
        //Do Nothing
    }
    
    private void OnFinishGame()
    {
        //Do Nothing
    }
    
    public void OnDown()
    {
        
    }

}
