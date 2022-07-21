using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    [System.Serializable]
    public class AllItemsList
    {
        public List<Character> Characters;
        public List<Item> Hairs;
        public List<Item> Heads;
        public List<Item> Chests;
        public List<Item> Pants;
        public List<Item> Shoes;
    }
    
    [System.Serializable]
    public class Item
    {
        public string name;
        public GameObject prefab;
        public int price;
        public ItemType ItemType;
    }
    
    [System.Serializable]
    public class Character
    {
        public string name;
        public GameObject prefab;
        public int price;
        public float scaleMultiplier;
        public CharacterType characterType;
    }
    
    [System.Serializable]
    public enum CharacterType
    {
        Giant,
        Dwarf
    }
    
    [System.Serializable]
    public enum ItemType
    {
        Hair,
        Head,
        Chest,
        Pant,
        Shoe
    }
    
}