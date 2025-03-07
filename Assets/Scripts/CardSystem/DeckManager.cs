using UnityEngine;
using System.Collections.Generic;
using Zenject;
using Assets.Scripts.Core;
using Assets.Scripts.Players;

namespace Assets.Scripts.CardSystem
{
    public class DeckManager : MonoBehaviour
    {
        public List<CardData> availableCards;
        public Transform handTransform;
        public GameObject cardPrefab;

        private List<CardData> playerHand = new List<CardData>();
        private List<Card> playerCards = new List<Card>();

        [SerializeField] private Enemy enemy;
        [SerializeField] private Player player;

        [SerializeField] private GameManager gameManager;

        [Inject]
        public void Construct(GameManager gameManager)
        {
            this.gameManager = gameManager;
            this.gameManager.OnUsersCreated += OnPlayerCreated;
            this.gameManager.OnGameStarted += StartGame;
        }

        private void OnPlayerCreated(Enemy enemy, Player player)
        {
            this.enemy = enemy;
            this.player = player;
            foreach (var card in playerCards){
                card.InjectEnemyAndPlayer(enemy, player);
            }
        }

        void StartGame()
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

            Card card = newCard.GetComponent<Card>();
            card.Initialize(selectedCard);
            card.InjectEnemyAndPlayer(enemy, player);

            playerCards.Add(card);
        }

        public void UnsubEvents()
        {
            this.gameManager.OnUsersCreated -= OnPlayerCreated;
            this.gameManager.OnGameStarted -= StartGame;
        }

        void OnDestroy()
        {
            UnsubEvents();
        }
    }
}