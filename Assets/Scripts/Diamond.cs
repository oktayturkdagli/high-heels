using UnityEngine;

namespace Game
{
    public class Diamond : Collectable
    {
        private void OnTriggerEnter(Collider other)
        {
            PutInPool(gameObject);
            EventManager.Instance.OnCollectAnItem(CollectableItemType.Diamond);
        }
    }
}