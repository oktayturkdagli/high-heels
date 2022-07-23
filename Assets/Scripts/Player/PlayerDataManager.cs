using System;
using System.Collections.Generic;
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
        }

        private void OnDisable()
        {
            EventManager.Instance.onCollectAnItem -= OnCollectAnItem;
        }

        private void LoadInitialValues()
        {
            LoadPlayerData();
            if (playerDataContainer == null || playerDataContainer.playerData == null)
            {
                playerDataContainer = new PlayerDataContainer();
                PlayerData tempPlayerData = new PlayerData();
                tempPlayerData.name = "Player1";
                tempPlayerData.level = 1;
                tempPlayerData.diamond = 0;
                tempPlayerData.itemsList = new ItemsList();
                playerDataContainer.playerData = tempPlayerData;
                SavePlayerData();
            } 
        }
        
        private void LoadPlayerData()
        {
            FileManager.Instance.ReadPlayerData(this);
        }

        private void SavePlayerData()
        {
            FileManager.Instance.WritePlayerData(playerDataContainer);
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
            SavePlayerData();
        }
        
    }
}