using System;
using System.Collections;
using Assets.Scripts.Players;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core
{
    public class TurnManager : MonoBehaviour
    {
        public Action<GameState> OnGameStateChange;
        [SerializeField] private TurnState currentState;
        [SerializeField] private GameState currentGameState;

        [SerializeField] private float turnDuration = 10f;

        [SerializeField] private GameManager gameManager;

        public Enemy enemy;
        public Player player;

        private Coroutine turnTimerCoroutine;
        private float currentTurnTime;
        public GameState CurrentGameState
        {
            get => currentGameState;
            set => currentGameState = value;
        }
        public bool IsPlayerDone { get; set; }

        [Inject]
        public void Construct(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        void Start()
        {
            CurrentGameState = GameState.NotStarted;
            SubEvents();
        }

        private void SubEvents(){
            gameManager.OnUsersCreated += OnPlayerCreated;
            gameManager.OnGameStarted += StartGame;
        }

        private void OnPlayerCreated(Enemy enemy, Player player)
        {
            this.enemy = enemy;
            this.player = player;
        }

        public void StartGame()
        {
            if (CurrentGameState == GameState.NotStarted)
            {
                Debug.Log("Game Started!");
                SwitchState(new PlayerTurnState());
            }
        }

        public void SwitchState(TurnState newState)
        {
            if (turnTimerCoroutine != null)
            {
                StopCoroutine(turnTimerCoroutine);
            }

            if (currentState != null)
            {
                currentState.ExitState(this, player, enemy);
            }

            currentState = newState;
            CurrentGameState = (newState is PlayerTurnState) ? GameState.PlayerTurn :
                               (newState is EnemyTurnState) ? GameState.EnemyTurn :
                               GameState.NotStarted;

            OnGameStateChange?.Invoke(CurrentGameState);

            currentState.EnterState(this, player, enemy);

            turnTimerCoroutine = StartCoroutine(TurnTimer());
        }

        private IEnumerator TurnTimer()
        {
            currentTurnTime = turnDuration;

            while (currentTurnTime > 0)
            {
                yield return new WaitForSeconds(1f);
                currentTurnTime--;
                Debug.Log($"Time Left: {currentTurnTime}");
            }

            Debug.Log("Turn Time Over! Switching turn...");
            SwitchState(CurrentGameState == GameState.PlayerTurn ? new EnemyTurnState() : new PlayerTurnState());
        }

        void Update()
        {
            if (currentState != null)
            {
                currentState.UpdateState(this, player, enemy);
            }
        }

        private void UnsubEvents(){
            gameManager.OnUsersCreated -= OnPlayerCreated;
            gameManager.OnGameStarted -= StartGame;
        }

        void OnDestroy()
        {
            UnsubEvents();
        }
    }
}