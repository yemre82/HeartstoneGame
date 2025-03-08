using UnityEngine;
using System.Collections.Generic;
using Zenject;
using Assets.Scripts.CardSystem;
using Assets.Scripts.Players;
using System;

namespace Assets.Scripts.Core
{
    [Serializable]
    public class DeckManager
    {
        public List<CardData> availableCards;
        public Transform playerHandTransform;
        public Transform enemyHandTransform;
        public GameObject cardPrefab;

        private List<CardData> playerHand = new List<CardData>();
        private List<CardData> enemyHand = new List<CardData>();

        private List<Card> playerCards = new List<Card>();
        private List<Card> enemyCards = new List<Card>();

        [SerializeField] private Enemy enemy;
        [SerializeField] private Player player;

        public void OnEntitiesCreated(Enemy enemy, Player player)
        {
            this.enemy = enemy;
            this.player = player;

            foreach (var card in playerCards)
            {
                card.InjectEnemyAndPlayer(enemy, player);
            }

            foreach (var card in enemyCards)
            {
                card.InjectEnemyAndPlayer(enemy, player);
            }
        }

        public void StartGame()
        {
            DrawStartingHand(playerHand, playerCards, playerHandTransform, 3);
            DrawStartingHand(enemyHand, enemyCards, enemyHandTransform, 3);
        }

        public void DrawStartingHand(List<CardData> hand, List<Card> cardObjects, Transform handTransform, int count)
        {
            for (int i = 0; i < count; i++)
            {
                DrawCard(hand, cardObjects, handTransform);
            }
        }

        public void DrawCard(List<CardData> hand, List<Card> cardObjects, Transform handTransform)
        {
            if (availableCards.Count == 0) return;

            int randomIndex = UnityEngine.Random.Range(0, availableCards.Count);
            CardData selectedCard = availableCards[randomIndex];

            hand.Add(selectedCard);

            GameObject newCard = UnityEngine.Object.Instantiate(cardPrefab, handTransform);

            Card card = newCard.GetComponent<Card>();
            card.Initialize(selectedCard);
            card.InjectEnemyAndPlayer(enemy, player);

            cardObjects.Add(card);
        }
    }
}
