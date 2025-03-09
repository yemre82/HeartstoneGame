using UnityEngine;
using Zenject;
using Assets.Scripts.Players;
using Assets.Scripts.CardSystem;
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
            StartCoroutine(EnemyAction());
        }

        private IEnumerator EnemyAction()
        {
            yield return new WaitForSeconds(1f);

            if (deckManager.GetEnemyCards().Count == 0){
                if (deckManager.HasCardsLeft())
                    deckManager.PullCard(deckManager.enemyHandPanel, deckManager.GetEnemyCards());
                else
                    yield return null;
            }

            Card chosenCard = deckManager.GetEnemyCards()[UnityEngine.Random.Range(0, deckManager.GetEnemyCards().Count)];
            deckManager.CardPlayedHandler(chosenCard);
        }

        private void OnDestroy()
        {
            UnregisterEvents();
        }
    }
}
