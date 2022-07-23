using UnityEngine;

namespace Game
{
    public abstract class Collectable : MonoBehaviour
    {
        protected void PutInPool(GameObject obj)
        {
            ObjectPool.Instance.PutInPool(obj);
        }
    }

    public enum CollectableItemType
    {
        Diamond,
        Shoe
    }
}