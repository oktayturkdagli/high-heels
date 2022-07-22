using UnityEngine;

namespace Game
{
    public class Shoe : Collectable
    {
        private void OnTriggerEnter(Collider other)
        {
            PutInPool(gameObject);
            EventManager.Instance.OnCollectAnItem(CollectableItemType.Shoe);
        }
    }
}