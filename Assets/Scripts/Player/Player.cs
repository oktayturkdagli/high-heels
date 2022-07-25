using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private CharacterType characterType = CharacterType.Ordinary;
        [SerializeField] private int speed = 2;
        [SerializeField] private bool canMove = false;
        [SerializeField] private Vector3 direction = Vector3.forward;
        [SerializeField] private float horizontalBorderSize = 5;
        [SerializeField] private PlayerItem necklace = new PlayerItem();
        [SerializeField] private PlayerItem bracelet = new PlayerItem();
        [SerializeField] private PlayerItem earring = new PlayerItem();
        private Animator playerAnimator;
        private static readonly int AnimatorParameterWalk = Animator.StringToHash("Walk");
        private static readonly int AnimatorParameterDance = Animator.StringToHash("Dance");
        private static readonly int AnimatorParameterBeSad = Animator.StringToHash("BeSad");

        private void Start()
        {
            playerAnimator = GetComponent<Animator>();
            UpdatePlayerItems();
        }

        private void OnEnable()
        {
            InputManager.Instance.onMove += OnFingerMove;
            InputManager.Instance.onStationary += OnFingerUp;
            InputManager.Instance.onUp += OnFingerUp;
            EventManager.Instance.onSwipeToRun += OnSwipeToRun;
            EventManager.Instance.onWinLevel += OnWinLevel;
            EventManager.Instance.onLoseLevel += OnLoseLevel;
        }

        private void OnDisable()
        {
            InputManager.Instance.onMove -= OnFingerMove;
            InputManager.Instance.onStationary -= OnFingerUp;
            InputManager.Instance.onUp -= OnFingerUp;
            EventManager.Instance.onSwipeToRun -= OnSwipeToRun;
            EventManager.Instance.onWinLevel -= OnWinLevel;
            EventManager.Instance.onLoseLevel -= OnLoseLevel;
        }
        
        private void OnFingerMove(Vector2 delta)
        {
            if (!canMove) return;
            var value = Math.Clamp(delta.x, -1f, 1f);
            direction.x = 20 * value;
        }
        
        private void OnFingerUp()
        {
            if (!canMove) return;
            direction.x = 0;
        }
        
        private void OnSwipeToRun()
        {
            StartCoroutine(ChangeCanMoveDelayed(0.3f, true));
        }
        
        private void OnWinLevel()
        {
            Dance();
        }
        
        private void OnLoseLevel()
        {
            BeSad();
        }

        private void Update()
        {
            Walk();
        }
        
        public void UpdatePlayerItems()
        {
            PlayerDataManager.Instance.LoadData();
            UpdatePlayerItem(ItemType.Necklace);
            UpdatePlayerItem(ItemType.Bracelet);
            UpdatePlayerItem(ItemType.Earring);
        }
        
        private void UpdatePlayerItem(ItemType itemType)
        {
            float multiplier = 1;
            if (characterType == CharacterType.Giant)
                multiplier = 2;
            else if (characterType == CharacterType.Dwarf)
                multiplier = 0.5f;

            ItemsList itemList = ItemDataManager.Instance.ItemDataContainer.itemData.itemsList;
            PlayerItem playerItem = null;
            List<Item> items = null;
            if (itemType == ItemType.Necklace)
            {
                playerItem = necklace;
                items = new List<Item>(itemList.necklaces);
            }
            else if (itemType == ItemType.Bracelet)
            {
                playerItem = bracelet;
                items = new List<Item>(itemList.bracelets);
            }
            else if (itemType == ItemType.Earring)
            {
                playerItem = earring;
                items = new List<Item>(itemList.earrings);
            }
            else
            {
                return;
            }
            
            int itemId = -1;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].doesPlayerHave)
                {
                    itemId = items[i].id;
                    break;
                }
            }
            
            if (itemId == -1) return;
            List<SceneItem> sceneItems = ItemDataManager.Instance.SceneItems;
            SceneItem sceneItem = sceneItems.FirstOrDefault(o => o.id == itemId);
            if (sceneItem == null)
                return;
            
            for (int i = 0; i < playerItem.parentObj.transform.childCount; i++)
                playerItem.parentObj.transform.GetChild(i).gameObject.SetActive(false);
            
            sceneItem.prefab.transform.parent = playerItem.parentObj.transform;
            sceneItem.prefab.SetActive(true);
            sceneItem.prefab.transform.localPosition = Vector3.zero;
            sceneItem.prefab.transform.localScale = new Vector3(multiplier, multiplier, multiplier);
            playerItem.prefabObj = sceneItem.prefab;
        }
        
        private void Walk()
        {
            if (!canMove) return;
            
            // Borders
            switch (direction.x)
            {
                case > 0 when transform.position.x > (horizontalBorderSize/2 - 0.1f):
                case < 0 when transform.position.x < -(horizontalBorderSize/2 - 0.1f):
                    direction.x = 0;
                    break;
            }

            playerAnimator.SetBool(AnimatorParameterWalk, true);
            transform.Translate(direction * (speed * Time.deltaTime), Space.World);
        }

        private void Rise()
        {
            Debug.Log("Rise");
        }

        private void Fall()
        {
            Debug.Log("Fall");
        }

        private void BeSad()
        {
            canMove = false;
            playerAnimator.SetTrigger(AnimatorParameterBeSad);
        }

        private void Dance()
        {
            canMove = false;
            playerAnimator.SetTrigger(AnimatorParameterDance);
        }

        private void OnTriggerEnter(Collider other)
        {
            switch (other.tag)
            {
                case "Finish":
                    other.enabled = false;
                    EventManager.Instance.OnWinLevel();
                    break;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            switch (collision.gameObject.tag)
            {
                case "CubeRed":
                    EventManager.Instance.OnLoseLevel();
                    break;
            }
        }

        private IEnumerator ChangeCanMoveDelayed(float time, bool enable) 
        {
            yield return new WaitForSecondsRealtime(time); //Wait time second
            canMove = enable;
        }
        
    }
    
    [System.Serializable]
    public class PlayerItem
    {
        public GameObject parentObj;
        public GameObject prefabObj;
        public ItemType itemType;
    }
    
    [System.Serializable]
    public class SceneItem
    {
        public int id;
        public GameObject prefab;
        public Sprite sprite;
    }
    
    [System.Serializable]
    public enum CharacterType
    {
        Ordinary,
        Giant,
        Dwarf
    }
}