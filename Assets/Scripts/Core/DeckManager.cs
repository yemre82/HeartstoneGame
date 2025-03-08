using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.CardSystem;
using Assets.Scripts.Players;
using System;

namespace Assets.Scripts.Core
{
    [Serializable]
    public class DeckManager
    {
        public List<CardData> availableCards;
        public Transform deckPanel;
        public Transform playerHandPanel;
        public Transform enemyHandPanel;
        public GameObject cardPrefab;

        private Queue<CardData> deck = new Queue<CardData>(); // 20 kartlık deste
        private List<Card> playerCards = new List<Card>();
        private List<Card> enemyCards = new List<Card>();

        private Enemy enemy;
        private Player player;

        public void OnEntitiesCreated(Enemy enemy, Player player)
        {
            this.enemy = enemy;
            this.player = player;
        }

        private void GenerateDeck()
        {
            List<CardData> shuffledDeck = new List<CardData>();

            for (int i = 0; i < 20; i++) // 20 kartlık deste oluştur
            {
                int randomIndex = UnityEngine.Random.Range(0, availableCards.Count);
                shuffledDeck.Add(availableCards[randomIndex]);
            }

            shuffledDeck = ShuffleDeck(shuffledDeck);
            deck = new Queue<CardData>(shuffledDeck);
        }

        private List<CardData> ShuffleDeck(List<CardData> deckToShuffle)
        {
            for (int i = deckToShuffle.Count - 1; i > 0; i--)
            {
                int randomIndex = UnityEngine.Random.Range(0, i + 1);
                CardData temp = deckToShuffle[i];
                deckToShuffle[i] = deckToShuffle[randomIndex];
                deckToShuffle[randomIndex] = temp;
            }
            return deckToShuffle;
        }

        private void ShowDeckUI()
        {
            for (int i = 0; i < 20; i++)
            {
                GameObject newCard = UnityEngine.Object.Instantiate(cardPrefab, deckPanel);
                newCard.GetComponent<Card>().Initialize(availableCards[i % availableCards.Count]);
                newCard.transform.localScale = Vector3.one * 0.5f; // Kartları küçük göster
            }
        }

        public void StartGame()
        {
            GenerateDeck();
            ShowDeckUI();
            CoroutineRunner.Instance.StartRoutine(DistributeStartingHand());
        }

        private IEnumerator DistributeStartingHand()
        {
            for (int i = 0; i < 5; i++)
            {
                DrawCard(playerHandPanel, playerCards);
                yield return new WaitForSeconds(0.5f);
                DrawCard(enemyHandPanel, enemyCards);
                yield return new WaitForSeconds(0.5f);
            }
        }

        public void DrawCard(Transform handPanel, List<Card> cardObjects)
        {
            if (deck.Count == 0)
            {
                Debug.Log("No more cards in the deck!");
                return;
            }

            CardData selectedCard = deck.Dequeue();

            GameObject newCard = UnityEngine.Object.Instantiate(cardPrefab, deckPanel);
            newCard.GetComponent<Card>().Initialize(selectedCard);
            newCard.GetComponent<Card>().InjectEnemyAndPlayer(enemy, player);
            CoroutineRunner.Instance.StartRoutine(MoveCardToHand(newCard, handPanel, cardObjects));
        }

        private IEnumerator MoveCardToHand(GameObject cardObject, Transform targetPanel, List<Card> cardObjects)
        {
            Vector3 startPosition = cardObject.transform.position;
            Vector3 targetPosition = targetPanel.position;
            float duration = 0.5f;
            float elapsed = 0;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                cardObject.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
                yield return null;
            }

            cardObject.transform.SetParent(targetPanel);
            cardObject.transform.localScale = Vector3.one;
            cardObjects.Add(cardObject.GetComponent<Card>());
        }
    }
}
