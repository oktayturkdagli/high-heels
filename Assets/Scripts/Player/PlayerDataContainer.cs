namespace Game
{
    [System.Serializable]
    public class PlayerDataContainer
    {
        public PlayerData playerData;
    }
    
    [System.Serializable]
    public class PlayerData
    {
        public string name;
        public int level;
        public int diamond;
    }
    
}