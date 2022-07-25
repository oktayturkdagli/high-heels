namespace Game
{
    [System.Serializable]
    public abstract class Item
    {
        public int id;
        public string title;
        public int price;
        public bool doesPlayerHave = false;
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