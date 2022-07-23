using System.Collections;
using TMPro;
using UnityEngine;

namespace Game
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI diamondText;
        [SerializeField] private Canvas startCanvas, gameCanvas, shopCanvas, winCanvas, loseCanvas;
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
            EventManager.Instance.onSwipeToRun += OnSwipeToRun;
            EventManager.Instance.onCollectAnItem += OnCollectAnItem;
            EventManager.Instance.onWinLevel += OnWinLevel;
            EventManager.Instance.onLoseLevel += OnLoseLevel;
        }

        private void OnDisable()
        {
            EventManager.Instance.onCollectAnItem -= OnCollectAnItem;
            EventManager.Instance.onSwipeToRun -= OnSwipeToRun;
            EventManager.Instance.onWinLevel -= OnWinLevel;
            EventManager.Instance.onLoseLevel -= OnLoseLevel;
        }

        private void LoadInitialValues()
        {
            PlayerData playerData = PlayerDataManager.Instance.PlayerDataContainer.playerData;
            levelText.text = "LEVEL " + playerData.level;
            diamondText.text = playerData.diamond.ToString();
        }
        
        private void OnSwipeToRun()
        {
            HideCanvas(startCanvas);
        }
        
        private void OnCollectAnItem(CollectableItemType collectableItemType)
        {
            if (collectableItemType == CollectableItemType.Diamond)
            {
                diamondText.text = (int.Parse(diamondText.text) + 1).ToString(); // Increase diamond count
            }
        }
        
        private void OnWinLevel()
        {
            HideCanvas(gameCanvas);
            ShowCanvas(winCanvas);
        }
        
        private void OnLoseLevel()
        {
            HideCanvas(gameCanvas);
            ShowCanvas(loseCanvas);
        }

        public void ShowCanvas(Canvas canvas)
        {
            StartCoroutine(ProcessCanvas(canvas, true));
        }

        public void HideCanvas(Canvas canvas)
        {
            StartCoroutine(ProcessCanvas(canvas, false));
        }
        
        private IEnumerator ProcessCanvas(Canvas canvas, bool enable)
        {
            yield return new WaitForSeconds(0.2f);
            canvas.enabled = enable;
        }

    }
}