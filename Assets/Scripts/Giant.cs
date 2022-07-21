using UnityEngine;

namespace Game
{
    public class Giant : Character
    {
        public Giant(int multiplier) : base(multiplier)
        {
        }

        public override void DoSpecialThing()
        {
            Debug.Log("Giant DoSpecialThing");
        }
        
    }
}