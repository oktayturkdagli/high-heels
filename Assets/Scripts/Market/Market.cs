using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Market : MonoBehaviour
    {
        private MarketData marketData;
        [SerializeField] private List<MarketItem> necklaces = new List<MarketItem>();
        [SerializeField] private List<MarketItem> bracelets = new List<MarketItem>();
        [SerializeField] private List<MarketItem> earrings = new List<MarketItem>();
        public MarketData MarketData { get => marketData; set => marketData = value; }
        public static Market Instance { get; set; }

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

        public void UpdateMarket()
        {
            UpdateList(ItemType.Necklace);
            UpdateList(ItemType.Bracelet);
            UpdateList(ItemType.Earring);
        }
        
        private void UpdateList(ItemType itemType)
        {
            ItemsList itemList = MarketDataManager.Instance.MarketDataContainer.marketData.itemsList;
            List<MarketItem> processingList = necklaces;
            List<Item> dataReceivingList = new List<Item>(itemList.necklaces); 
            if (itemType == ItemType.Bracelet)
            {
                processingList = bracelets;
                dataReceivingList = new List<Item>(itemList.bracelets);
            }
            else if (itemType == ItemType.Earring)
            {
                processingList = earrings;
                dataReceivingList = new List<Item>(itemList.earrings);
            }
            
            if (processingList.Count < 1)
                return;
            
            for (var i = 0; i < processingList.Count; i++)
            {
                if (i > dataReceivingList.Count - 1)
                    return;
                    
                processingList[i].title.text = dataReceivingList[i].title.ToUpper().Replace("_", " ");
                processingList[i].image.sprite = dataReceivingList[i].sprite;
                processingList[i].price.text = dataReceivingList[i].price.ToString();
            }
        }
        
    }
    
    [System.Serializable]
    public class MarketItem
    {
        public TextMeshProUGUI title;
        public Image image;
        public TextMeshProUGUI price;
        public ItemType itemType;
    }
}