using UnityEngine;

namespace Game
{
    [System.Serializable]
    public abstract class Item
    {
        public string title;
        public GameObject prefab;
        public Sprite sprite;
        public int price;
        public ItemType itemType;
    }
    
    [System.Serializable]
    public enum ItemType
    {
        Necklace,
        Bracelet,
        Earring
    }
}