using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Assets.Scripts.CardSystem
{
    public class Card : MonoBehaviour
    {
        public Action<Card> OnCardPlayed;

        public CardData cardData;

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

        public void PlayCard()
        {
            OnCardPlayed?.Invoke(this);
        }
    }
}