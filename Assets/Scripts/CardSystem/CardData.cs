using UnityEngine;

namespace Assets.Scripts.CardSystem
{
    public enum CardType
    {
        Attack,
        Heal,
        Buff,
        Debuff
    }

    [CreateAssetMenu(fileName = "NewCard", menuName = "Card System/New Card")]
    public class CardData : ScriptableObject
    {
        public string cardName;
        public int manaCost;
        public string description;
        public Sprite artwork;
        public CardType cardType;
        public int effectValue;
    }
}