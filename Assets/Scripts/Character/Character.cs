using UnityEngine;

namespace Game
{
    public class Character
    {
        public CharacterType CharacterType;
        public float ScaleMultiplier;
    }

    public enum CharacterType
    {
        Ordinary,
        Giant,
        Dwarf
    }
}