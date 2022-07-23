using UnityEngine;

namespace Game
{
    public class MarketDataManager : MonoBehaviour
    {
        [SerializeField] private MarketDataContainer marketDataContainer;
        
        public MarketDataContainer MarketDataContainer { get => marketDataContainer; set => marketDataContainer = value; }
        public static MarketDataManager Instance { get; set; }

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

        private void LoadInitialValues()
        {
            LoadData();
            if (marketDataContainer == null || marketDataContainer.marketData == null)
            {
                marketDataContainer = new MarketDataContainer();
                marketDataContainer.marketData = new MarketData();
                marketDataContainer.marketData.itemsList = new ItemsList();
                AddNecklaces(marketDataContainer.marketData.itemsList);
                AddBracelets(marketDataContainer.marketData.itemsList);
                AddEarrings(marketDataContainer.marketData.itemsList);
                SaveData();
            } 
        }
        
        private void LoadData()
        {
            FileManager.Instance.ReadData(this, DataType.Market);
        }

        public void SaveData()
        {
            FileManager.Instance.WriteData(this, DataType.Market);
            LoadData();
        }

        private void AddNecklaces(ItemsList itemList)
        {
            Necklace tempItem = new Necklace();
            tempItem.title = "Necklace_A";
            tempItem.prefab = null;
            tempItem.sprite = null;
            tempItem.price = 1;
            tempItem.itemType = ItemType.Necklace;
            itemList.necklaces.Add(tempItem);

            tempItem = new Necklace();
            tempItem.title = "Necklace_B";
            tempItem.prefab = null;
            tempItem.sprite = null;
            tempItem.price = 5;
            tempItem.itemType = ItemType.Necklace;
            itemList.necklaces.Add(tempItem);
            
            tempItem = new Necklace();
            tempItem.title = "Necklace_C";
            tempItem.prefab = null;
            tempItem.sprite = null;
            tempItem.price = 7;
            tempItem.itemType = ItemType.Necklace;
            itemList.necklaces.Add(tempItem);
            
        }
        
        private void AddBracelets(ItemsList itemList)
        {
            Bracelet tempItem = new Bracelet();
            tempItem.title = "Bracelet_A";
            tempItem.prefab = null;
            tempItem.sprite = null;
            tempItem.price = 1;
            tempItem.itemType = ItemType.Bracelet;
            itemList.bracelets.Add(tempItem);
            
            tempItem = new Bracelet();
            tempItem.title = "Bracelet_B";
            tempItem.prefab = null;
            tempItem.sprite = null;
            tempItem.price = 4;
            tempItem.itemType = ItemType.Bracelet;
            itemList.bracelets.Add(tempItem);
            
            tempItem = new Bracelet();
            tempItem.title = "Bracelet_C";
            tempItem.prefab = null;
            tempItem.sprite = null;
            tempItem.price = 3;
            tempItem.itemType = ItemType.Bracelet;
            itemList.bracelets.Add(tempItem);
            
        }
        
        private void AddEarrings(ItemsList itemList)
        {
            Earring tempItem = new Earring();
            tempItem.title = "Earring_A";
            tempItem.prefab = null;
            tempItem.sprite = null;
            tempItem.price = 1;
            tempItem.itemType = ItemType.Earring;
            itemList.earrings.Add(tempItem);
            
            tempItem = new Earring();
            tempItem.title = "Earring_B";
            tempItem.prefab = null;
            tempItem.sprite = null;
            tempItem.price = 3;
            tempItem.itemType = ItemType.Earring;
            itemList.earrings.Add(tempItem);
            
            tempItem = new Earring();
            tempItem.title = "Earring_C";
            tempItem.prefab = null;
            tempItem.sprite = null;
            tempItem.price = 6;
            tempItem.itemType = ItemType.Earring;
            itemList.earrings.Add(tempItem);

        }
        
        private void OnDisable()
        {
            SaveData();
        }
        
    }
}