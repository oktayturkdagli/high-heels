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
             
        }
        
        private void LoadLevelData()
        {
            FileManager.Instance.ReadData(this, DataType.Market);
        }

        public void SaveLevelData()
        {
            FileManager.Instance.ReadData(this, DataType.Market);
            LoadLevelData();
        }
        
    }
}