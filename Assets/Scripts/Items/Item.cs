using UnityEngine;

namespace Game
{
    public abstract class Item : MonoBehaviour
    {
        public string title;
        public GameObject prefab;
        public int price;
    }
}