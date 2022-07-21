using System.Collections.Generic;

namespace Game
{
    [System.Serializable]
    public class PlayerDataContainer
    {
        public List<PlayerData> playerDataList;
    }
    
    [System.Serializable]
    public class PlayerData
    {
        public string name;
        public int level;
        public int diamond;
        public AllItemsList allItemsList;
    }
    
}