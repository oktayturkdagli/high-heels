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
        public event Action onSwipeToRun;
        public event Action onWinLevel;
        public event Action onLoseLevel;
        public event Action<CollectableItemType> onCollectAnItem;
        
        
        //Events cannot be triggered directly from another class so they are triggered via functions
        public void OnSwipeToRun()
        {
            onSwipeToRun?.Invoke();
        }
        
        public void OnWinLevel()
        {
            onWinLevel?.Invoke();
        }
        
        public void OnLoseLevel()
        {
            onLoseLevel?.Invoke();
        }
        
        public void OnCollectAnItem(CollectableItemType collectableItemType)
        {
            onCollectAnItem?.Invoke(collectableItemType);
        }
        
    }
}

