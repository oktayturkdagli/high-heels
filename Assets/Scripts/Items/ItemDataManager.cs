using UnityEditor;
using UnityEngine;

namespace Game
{
    public class ItemDataManager : MonoBehaviour
    {
        [SerializeField] private ItemDataContainer itemDataContainer;
        
        public ItemDataContainer ItemDataContainer { get => itemDataContainer; set => itemDataContainer = value; }
        public static ItemDataManager Instance { get; set; }

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
            if (itemDataContainer == null || itemDataContainer.itemData == null)
            {
                itemDataContainer = new ItemDataContainer();
                itemDataContainer.itemData = new ItemData();
                itemDataContainer.itemData.itemsList = new ItemsList();
                SaveData();
            } 
        }
        
        public void LoadData()
        {
            FileManager.Instance.ReadData(this, DataType.Item);
        }

        public void SaveData()
        {
            FileManager.Instance.WriteData(this, DataType.Item);
            LoadData();
        }

    }
    
#if UNITY_EDITOR
    [CustomEditor(typeof(ItemDataManager))]
    public class ItemDataManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            ItemDataManager myScript = (ItemDataManager)target;
            EditorGUILayout.Space(20);
            if(GUILayout.Button("Save"))
            {
                myScript.SaveData();
            }
        }
    }
#endif
    
}