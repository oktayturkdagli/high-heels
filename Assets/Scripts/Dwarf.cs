using UnityEngine;

namespace Game
{
    public class Dwarf : Character
    {
        public Dwarf(int multiplier) : base(multiplier)
        {
        }

        public override void DoSpecialThing()
        {
            Debug.Log("Dwarf DoSpecialThing");
        }
        
    }
}