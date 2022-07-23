namespace Game
{
    [System.Serializable]
    public class Character
    {
        public CharacterType CharacterType;
        public float ScaleMultiplier;
    }
    
    [System.Serializable]
    public enum CharacterType
    {
        Ordinary,
        Giant,
        Dwarf
    }
}