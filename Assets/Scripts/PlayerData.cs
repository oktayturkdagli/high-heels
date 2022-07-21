using System.Collections.Generic;

namespace Game
{
    [System.Serializable]
    public class PlayerData
    {
        public string name = "Player1";
        public int level = 1;
        public int diamond = 0;
        public AllItemsList allItemsList;
    }

    [System.Serializable]
    public class PlayerDataContainer
    {
        public List<PlayerData> playerDataList;
    }
}