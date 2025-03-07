using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Players;

namespace Assets.Scripts.CardSystem
{
    public class Card : MonoBehaviour
    {
        public CardData cardData;

        public Player player;
        public Enemy enemy;

        [SerializeField] private TMP_Text cardNameText;
        [SerializeField] private TMP_Text manaCostText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private Image artworkImage;

        public void Initialize(CardData data)
        {
            cardData = data;
            cardNameText.text = data.cardName;
            manaCostText.text = data.manaCost.ToString();
            descriptionText.text = data.description;
            artworkImage.sprite = data.artwork;
        }

        public void InjectEnemyAndPlayer(Enemy enemy, Player player)
        {
            this.player = player;
            this.enemy = enemy;
        }
    }
}