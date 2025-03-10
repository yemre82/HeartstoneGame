using UnityEngine;
using Zenject;
using Assets.Scripts.Players;
using Assets.Scripts.CardSystem;
using Assets.Scripts.Audio;
using System;
using System.Collections;

namespace Assets.Scripts.Core
{
    public class GameManager : MonoBehaviour
    {
        public Action<Enemy, Player> OnUsersCreated;
        public Action OnGameStarted;

        public DeckManager deckManager;
        public TurnManager turnManager;

        public Enemy enemy;
        public Player player;

        private EnemyFactory enemyFactory;
        private PlayerFactory playerFactory;

        public bool isPlayerTurn = true;
        private bool enemyPlayed = false; // Çift çağrıyı önlemek için

        [Inject]
        public void Construct(EnemyFactory enemyFactory, PlayerFactory playerFactory)
        {
            this.enemyFactory = enemyFactory;
            this.playerFactory = playerFactory;
        }

        public void StartGame()
        {
            Debug.Log("Game Started! Creating Player and Enemy...");

            player = playerFactory.Create();
            enemy = enemyFactory.Create();

            OnUsersCreated?.Invoke(enemy, player);
            OnGameStarted?.Invoke();

            deckManager.OnEntitiesCreated(enemy, player);
            deckManager.StartGame();

            turnManager.OnPlayerCreated(enemy, player);
            turnManager.StartGame();

            RegisterEvents();
            Debug.Log("Player and Enemy Created Successfully.");
        }

        private void RegisterEvents()
        {
            turnManager.OnGameStateChange += TurnManager_OnGameStateChange;
        }

        private void UnregisterEvents()
        {
            turnManager.OnGameStateChange -= TurnManager_OnGameStateChange;
        }

        private void TurnManager_OnGameStateChange(GameState state)
        {
            if (state == GameState.PlayerTurn)
            {
                isPlayerTurn = true;
                enemyPlayed = false; // Yeni tur başladığında enemy tekrar oynayabilir
            }
            else
            {
                isPlayerTurn = false;
                EnemyTurn();
            }
        }

        public void PullCard()
        {
            if (!isPlayerTurn) return;
            deckManager.PullCard(deckManager.playerHandPanel, deckManager.GetPlayerCards());
        }

        public void EnemyTurn()
        {
            if (enemyPlayed) return; // Eğer enemy zaten oynadıysa tekrar oynamasına izin verme
            enemyPlayed = true;
            StartCoroutine(EnemyAction());
        }

        private IEnumerator EnemyAction()
        {
            yield return new WaitForSeconds(1f);

            if (deckManager.GetEnemyCards().Count == 0)
            {
                if (deckManager.HasCardsLeft())
                {
                    deckManager.PullCard(deckManager.enemyHandPanel, deckManager.GetEnemyCards());
                    yield return new WaitForSeconds(1f);
                }
                else
                {
                    yield return null;
                }
            }

            Card chosenCard = deckManager.GetEnemyCards()[UnityEngine.Random.Range(0, deckManager.GetEnemyCards().Count)];
            Debug.Log($"Enemy played: {chosenCard.cardData.cardName}");

            deckManager.CardPlayedHandler(chosenCard);
        }

        public void EndTurn()
        {
            if (turnManager.CurrentGameState != GameState.PlayerTurn) return;

            AudioManager.Instance.PlaySFX(AudioManager.Instance.endTurnSound);
            turnManager.SwitchState(new EnemyTurnState());
        }

        private void OnDestroy()
        {
            UnregisterEvents();
        }
    }
}
