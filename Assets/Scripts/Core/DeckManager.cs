using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.CardSystem;
using Assets.Scripts.Players;
using System;
using Assets.Scripts.Effects;

namespace Assets.Scripts.Core
{
    [Serializable]
    public class DeckManager
    {
        public List<CardData> availableCards;
        public Transform deckPanel;
        public Transform playerHandPanel;
        public Transform enemyHandPanel;
        public Transform playAreaPanel; // Oynanan kartların gideceği alan
        public GameObject cardPrefab;

        private Queue<Card> deck = new Queue<Card>();
        private List<GameObject> deckCardsUI = new List<GameObject>();
        private List<Card> playerCards = new List<Card>();
        private List<Card> enemyCards = new List<Card>();

        private Enemy enemy;
        private Player player;
        public GameManager gameManager;

        public bool HasCardsLeft()
        {
            return deck.Count > 0;
        }

        public List<Card> GetPlayerCards()
        {
            return playerCards;
        }

        public List<Card> GetEnemyCards()
        {
            return enemyCards;
        }

        public void OnEntitiesCreated(Enemy enemy, Player player)
        {
            this.enemy = enemy;
            this.player = player;

            this.enemy.OnIsDead += OnEnemyDefeated;
            this.player.OnIsDead += OnPlayerDefeated;
        }

        private void OnPlayerDefeated()
        {
            foreach (Card cards in playerCards)
            {
                UnityEngine.Object.Destroy(cards.gameObject);
            }
            player.OnIsDead -= OnPlayerDefeated;
        }

        private void OnEnemyDefeated()
        {
            foreach (Card cards in enemyCards)
            {
                UnityEngine.Object.Destroy(cards.gameObject);
            }
            enemy.OnIsDead -= OnEnemyDefeated;
        }

        private void GenerateDeck()
        {
            List<Card> shuffledDeck = new List<Card>();

            for (int i = 0; i < 20; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, availableCards.Count);
                GameObject newCardObject = UnityEngine.Object.Instantiate(cardPrefab, deckPanel);
                Card newCard = newCardObject.GetComponent<Card>();
                newCard.Initialize(availableCards[randomIndex]);
                newCardObject.transform.localScale = Vector3.one * 0.5f;
                newCard.OnCardPlayed += CardPlayedHandler;
                deckCardsUI.Add(newCardObject);
                shuffledDeck.Add(newCard);
            }

            shuffledDeck = ShuffleDeck(shuffledDeck);
            deck = new Queue<Card>(shuffledDeck);
        }

        private List<Card> ShuffleDeck(List<Card> deckToShuffle)
        {
            for (int i = deckToShuffle.Count - 1; i > 0; i--)
            {
                int randomIndex = UnityEngine.Random.Range(0, i + 1);
                Card temp = deckToShuffle[i];
                deckToShuffle[i] = deckToShuffle[randomIndex];
                deckToShuffle[randomIndex] = temp;
            }
            return deckToShuffle;
        }

        public void StartGame()
        {
            GenerateDeck();
            CoroutineRunner.Instance.StartRoutine(DistributeStartingHand());
        }

        private IEnumerator DistributeStartingHand()
        {
            for (int i = 0; i < 5; i++)
            {
                PullCard(playerHandPanel, playerCards);
                yield return new WaitForSeconds(0.5f);
                PullCard(enemyHandPanel, enemyCards);
                yield return new WaitForSeconds(0.5f);
            }
        }

        public void PullCard(Transform handPanel, List<Card> hand)
        {
            if (deck.Count == 0)
            {
                Debug.Log("No more cards in the deck!");
                return;
            }

            Card card = deck.Dequeue();
            GameObject cardObject = deckCardsUI[0];
            deckCardsUI.RemoveAt(0);
            CoroutineRunner.Instance.StartRoutine(MoveCardToHand(cardObject, handPanel, hand));

            Debug.Log($"Deck remaining: {deck.Count} cards.");
        }

        private IEnumerator MoveCardToHand(GameObject cardObject, Transform targetPanel, List<Card> hand)
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
            hand.Add(cardObject.GetComponent<Card>());
        }

        public void CardPlayedHandler(Card card)
        {
            Debug.Log($"Card Played: {card.cardData.cardName}");

            ICardEffect effect = null;
            Transform targetArea = playAreaPanel;

            if (gameManager.isPlayerTurn)
            {
                if (card.cardData.cardType == CardType.Attack)
                {
                    effect = new DamageEffect(card.cardData.effectValue, enemy, player, true);
                }
                else if (card.cardData.cardType == CardType.Heal)
                {
                    effect = new HealEffect(card.cardData.effectValue, player, enemy, true);
                }
                playerCards.Remove(card);
            }
            else
            {
                if (card.cardData.cardType == CardType.Attack)
                {
                    effect = new DamageEffect(card.cardData.effectValue, enemy, player, false);
                }
                else if (card.cardData.cardType == CardType.Heal)
                {
                    effect = new HealEffect(card.cardData.effectValue, player, enemy, false);
                }
                enemyCards.Remove(card);
            }

            effect?.ApplyEffect();
            card.OnCardPlayed -= CardPlayedHandler;
            CoroutineRunner.Instance.StartRoutine(MoveCardToPlayArea(card.gameObject, targetArea));
        }

        private IEnumerator MoveCardToPlayArea(GameObject cardObject, Transform targetArea)
        {
            Vector3 startPosition = cardObject.transform.position;
            Vector3 targetPosition = targetArea.position;
            float duration = 0.5f;
            float elapsed = 0;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                cardObject.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
                yield return null;
            }

            cardObject.transform.SetParent(targetArea);
            cardObject.transform.localScale = Vector3.one;

            yield return new WaitForSeconds(1.5f); // Play Area'da kart bir süre kalsın

            GameObject.Destroy(cardObject);
        }
    }
}
