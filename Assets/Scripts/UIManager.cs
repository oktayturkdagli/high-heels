using TMPro;
using UnityEngine;

namespace Game
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI diamondText;
        
        public static UIManager Instance { get; set; }
        
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
            PlayerData playerData = PlayerDataManager.Instance.PlayerDataContainer.playerData;
            levelText.text = "LEVEL " + playerData.level;
            diamondText.text = playerData.diamond.ToString();
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
            diamondText.text = (int.Parse(diamondText.text) + 1).ToString();
        }
        
        
    }
}