using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.CardSystem
{
    public class DeckManager : MonoBehaviour
    {
        public List<CardData> availableCards;
        public Transform handTransform;
        public GameObject cardPrefab;

        private List<CardData> playerHand = new List<CardData>();

        void Start()
        {
            DrawStartingHand(3);
        }

        public void DrawStartingHand(int count)
        {
            for (int i = 0; i < count; i++)
            {
                DrawCard();
            }
        }

        public void DrawCard()
        {
            if (availableCards.Count == 0) return;

            int randomIndex = Random.Range(0, availableCards.Count);
            CardData selectedCard = availableCards[randomIndex];

            playerHand.Add(selectedCard);

            GameObject newCard = Instantiate(cardPrefab, handTransform);
            newCard.GetComponent<Card>().Initialize(selectedCard);
        }
    }
}