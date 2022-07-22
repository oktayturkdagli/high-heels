using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; set; }
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
            EventManager.Instance.onLoadScene += OnLoadScene;
            EventManager.Instance.onStartLevel += OnStartLevel;
            EventManager.Instance.onFinishLevel += OnFinishLevel;
        }

        private void OnDisable()
        {
            EventManager.Instance.onStartLevel -= OnStartLevel;
            EventManager.Instance.onFinishLevel -= OnFinishLevel;
        }

        private void Start()
        {
            EventManager.Instance.OnLoadScene();
        }

        private void OnLoadScene()
        {
            LevelManager.Instance.DrawLevel(level);
            EventManager.Instance.OnStartLevel();
        }

        private void OnStartLevel()
        {
            EventManager.Instance.OnAllowPlayerMovement();
        }

        private void OnFinishLevel()
        {
            //Do Nothing
        }

        public void OnDown()
        {

        }
        
    }
}
