using UnityEngine;

namespace Game
{
    public class Market : MonoBehaviour
    {
        private MarketData marketData;
        
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

        public void Sell()
        {
            
        }
        
    }
}