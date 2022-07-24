using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Market : MonoBehaviour
    {
        private ItemData itemData;
        [SerializeField] private List<MarketItem> necklaces = new List<MarketItem>();
        [SerializeField] private List<MarketItem> bracelets = new List<MarketItem>();
        [SerializeField] private List<MarketItem> earrings = new List<MarketItem>();
        public ItemData ItemData { get => itemData; set => itemData = value; }
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
            ItemsList itemList = ItemDataManager.Instance.ItemDataContainer.itemData.itemsList;
            List<MarketItem> marketItems = necklaces;
            List<Item> items = new List<Item>(itemList.necklaces); 
            if (itemType == ItemType.Bracelet)
            {
                marketItems = bracelets;
                items = new List<Item>(itemList.bracelets);
            }
            else if (itemType == ItemType.Earring)
            {
                marketItems = earrings;
                items = new List<Item>(itemList.earrings);
            }
            
            if (marketItems.Count < 1)
                return;
            
            for (var i = 0; i < marketItems.Count; i++)
            {
                if (i > items.Count - 1)
                    return;
                    
                marketItems[i].title.text = items[i].title.ToUpper().Replace("_", " ");
                marketItems[i].image.sprite = items[i].sprite;
                marketItems[i].price.text = items[i].price.ToString();
                if (items[i].doesPlayerHave)
                {
                    marketItems[i].price.text = "USING";
                    marketItems[i].button.image.color = Color.gray;
                    marketItems[i].button.enabled = !items[i].doesPlayerHave;
                }
            }
        }
        
    }
    
    [System.Serializable]
    public class MarketItem
    {
        public TextMeshProUGUI title;
        public Image image;
        public TextMeshProUGUI price;
        public Button button;
    }
}