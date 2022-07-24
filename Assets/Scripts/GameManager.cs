using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; set; }
        private int level;
        
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
            InputManager.Instance.onSwipe += OnSwipe;
        }

        private void OnDisable()
        {
            InputManager.Instance.onSwipe -= OnSwipe;
        }

        private void Start()
        {
            level = PlayerDataManager.Instance.PlayerDataContainer.playerData.level;
            LevelDataManager.Instance.DrawLevel(level);
        }

        private void OnSwipe(SwipeType swipeType)
        {
            if (swipeType != SwipeType.Left && swipeType != SwipeType.Right)
                return;
            
            EventManager.Instance.OnSwipeToRun();
            
            InputManager.Instance.onSwipe -= OnSwipe;
        }
        
    }
}
