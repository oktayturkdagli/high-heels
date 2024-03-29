using UnityEngine;

namespace Game
{
    public class PlayerDataManager : MonoBehaviour
    {
        [SerializeField] private PlayerDataContainer playerDataContainer;
        
        public PlayerDataContainer PlayerDataContainer { get => playerDataContainer; set => playerDataContainer = value; }
        public static PlayerDataManager Instance { get; set; }
        
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
            
            LoadInitialValues();
        }
        
        private void OnEnable()
        {
            EventManager.Instance.onCollectAnItem += OnCollectAnItem;
            EventManager.Instance.onWinLevel += OnWinLevel;
        }

        private void OnDisable()
        {
            EventManager.Instance.onCollectAnItem -= OnCollectAnItem;
            EventManager.Instance.onWinLevel -= OnWinLevel;
        }

        private void OnWinLevel()
        {
            playerDataContainer.playerData.level += 1;
            SaveData();
        }

        private void LoadInitialValues()
        {
            LoadData();
            if (playerDataContainer == null || playerDataContainer.playerData == null)
            {
                playerDataContainer = new PlayerDataContainer();
                PlayerData tempPlayerData = new PlayerData();
                tempPlayerData.name = "Player1";
                tempPlayerData.level = 1;
                tempPlayerData.diamond = 0;
                playerDataContainer.playerData = tempPlayerData;
                SaveData();
            } 
        }
        
        public void LoadData()
        {
            FileManager.Instance.ReadData(this, DataType.Player);
        }

        private void SaveData()
        {
            FileManager.Instance.WriteData(this, DataType.Player);
        }
        
        private void OnCollectAnItem(CollectableItemType collectableItemType)
        {
            if (collectableItemType == CollectableItemType.Diamond)
            {
                IncreaseDiamondCount();
            }
        }
        
        private void IncreaseDiamondCount()
        {
            playerDataContainer.playerData.diamond += 1;
            SaveData();
        }
        
    }
}