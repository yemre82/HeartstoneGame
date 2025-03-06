using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.CardSystem
{
    public class Card : MonoBehaviour
    {
        public CardData cardData;

        [SerializeField] private Text cardNameText;
        [SerializeField] private Text manaCostText;
        [SerializeField] private Text descriptionText;
        [SerializeField] private Image artworkImage;

        public void Initialize(CardData data)
        {
            cardData = data;
            cardNameText.text = data.cardName;
            manaCostText.text = data.manaCost.ToString();
            descriptionText.text = data.description;
            artworkImage.sprite = data.artwork;
        }
    }
}