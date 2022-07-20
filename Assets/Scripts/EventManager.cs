using System;
using UnityEngine;

[ExecuteInEditMode]
public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }
    
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

    //Events are created
    public event Action onStartGame;
    public event Action onFinishGame;
    public event Action onWinGame;
    public event Action onLoseGame;

    //Events cannot be triggered directly from another class so they are triggered via functions
    public void OnStartGame()
    {
        onStartGame?.Invoke();
    }

    public void OnFinishGame()
    {
        onFinishGame?.Invoke();
    }
    
    public void OnWinGame()
    {
        onWinGame?.Invoke();
    }
    
    public void OnLoseGame()
    {
        onLoseGame?.Invoke();
    }
    
}

