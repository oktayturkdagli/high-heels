using UnityEngine;

namespace Game
{
    public abstract class Player
    {
        protected int multiplier;

        protected Player(int multiplier)
        {
            this.multiplier = multiplier;
        }

        protected void Walk()
        {
            Debug.Log("Walk");
        }

        protected void Rise()
        {
            Debug.Log("Rise");
        }

        protected void Fall()
        {
            Debug.Log("Fall");
        }

        protected void CollectDiamond()
        {
            Debug.Log("CollectDiamond");
        }

        protected void Die()
        {
            Debug.Log("Die");
        }

        protected void Dance()
        {
            Debug.Log("Dance");
        }

    }
}