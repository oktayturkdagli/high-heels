using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PlayerDataContainer playerDataContainer;
        
        public PlayerDataContainer PlayerDataContainer { get => playerDataContainer; set => playerDataContainer = value; }

        private void Awake()
        {
            LoadInitialValues();
        }
        
        private void OnEnable()
        {
            EventManager.Instance.onStartLevel += OnStartLevel;
        }

        private void OnDisable()
        {
            EventManager.Instance.onStartLevel -= OnStartLevel;
        }
        
        private void OnStartLevel()
        {
            //Do Nothing
        }
        
        private void LoadInitialValues()
        {
            LoadPlayerData();
            if (playerDataContainer.playerDataList == null || playerDataContainer.playerDataList.Count < 1)
            {
                playerDataContainer = new PlayerDataContainer();
                playerDataContainer.playerDataList = new List<PlayerData>();
                PlayerData tempPlayerData = new PlayerData();
                tempPlayerData.name = "Player1";
                tempPlayerData.level = 1;
                tempPlayerData.diamond = 0;
                tempPlayerData.allItemsList = new AllItemsList();
                playerDataContainer.playerDataList.Add(tempPlayerData);
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

        
    }
}