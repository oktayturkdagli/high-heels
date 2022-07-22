using System;
using UnityEngine;

namespace Game
{
    [ExecuteInEditMode]
    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance { get; set; }

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
        public event Action onLoadScene;
        public event Action onStartLevel;
        public event Action onFinishLevel;
        public event Action onWinLevel;
        public event Action onLoseLevel;
        public event Action onAllowPlayerMovement;
        
        
        //Events cannot be triggered directly from another class so they are triggered via functions
        public void OnLoadScene()
        {
            onLoadScene?.Invoke();
        }
        
        public void OnStartLevel()
        {
            onStartLevel?.Invoke();
        }
        
        public void OnFinishLevel()
        {
            onFinishLevel?.Invoke();
        }
        
        public void OnWinLevel()
        {
            onWinLevel?.Invoke();
        }
        
        public void OnLoseLevel()
        {
            onLoseLevel?.Invoke();
        }
        
        public void OnAllowPlayerMovement()
        {
            onAllowPlayerMovement?.Invoke();
        }
        
    }
}

