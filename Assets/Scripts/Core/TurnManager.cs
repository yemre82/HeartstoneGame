using System;
using System.Collections;
using Assets.Scripts.Players;
using UnityEngine;

namespace Assets.Scripts.Core
{
    [Serializable]
    public class TurnManager
    {
        public Action<GameState> OnGameStateChange;
        public Action<float> OnTurnTimeChange;
        [SerializeField] private TurnState currentState;
        [SerializeField] private GameState currentGameState;

        public float turnDuration = 10f;

        private Enemy enemy;
        private Player player;

        private IEnumerator turnTimerCoroutine;
        public float currentTurnTime;
        public GameState CurrentGameState
        {
            get => currentGameState;
            set => currentGameState = value;
        }
        public bool IsPlayerDone { get; set; }

        public void OnPlayerCreated(Enemy enemy, Player player)
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
                CoroutineRunner.Instance.StopRoutine(turnTimerCoroutine);
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

            turnTimerCoroutine = TurnTimer();
            CoroutineRunner.Instance.StartRoutine(turnTimerCoroutine);
        }

        private IEnumerator TurnTimer()
        {
            currentTurnTime = turnDuration;

            while (currentTurnTime > 0)
            {
                yield return new WaitForSeconds(1f);
                currentTurnTime--;
                OnTurnTimeChange?.Invoke(currentTurnTime);
                Debug.Log($"Time Left: {currentTurnTime}");
            }

            Debug.Log("Turn Time Over! Switching turn...");
            SwitchState(CurrentGameState == GameState.PlayerTurn ? new EnemyTurnState() : new PlayerTurnState());
        }
    }
}
