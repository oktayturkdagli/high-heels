using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    [System.Serializable]
    public struct AllItemsList
    {
        public List<Character> Characters;
        public List<Item> Hairs;
        public List<Item> Heads;
        public List<Item> Chests;
        public List<Item> Pants;
        public List<Item> Shoes;
    }
    
    public struct Item
    {
        public string name;
        public GameObject prefab;
        public int price;
        public ItemType ItemType;
    }
    
    public struct Character
    {
        public string name;
        public GameObject prefab;
        public int price;
        public CharacterType characterType;
    }
    
    public enum CharacterType
    {
        Giant,
        Dwarf
    }
    
    public enum ItemType
    {
        Character,
        Hair,
        Head,
        Chest,
        Pant,
        Shoe
    }
    
}