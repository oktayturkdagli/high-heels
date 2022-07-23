using System.Collections.Generic;

namespace Game
{
    [System.Serializable]
    public class ItemsList
    {
        public List<Necklace> necklaces = new List<Necklace>();
        public List<Bracelet> bracelets = new List<Bracelet>();
        public List<Earring> earrings = new List<Earring>();
    }

}