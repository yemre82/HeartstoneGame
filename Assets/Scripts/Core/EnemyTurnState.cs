using UnityEngine;
using System.Collections;
using Assets.Scripts.Players;

namespace Assets.Scripts.Core
{
    public class EnemyTurnState : TurnState
    {
        private bool isPlaying = false;

        public override void EnterState(TurnManager manager, Player player, Enemy enemy, GameManager gameManager = null)
        {
            if (isPlaying) return;
            isPlaying = true;

            Debug.Log("Enemy's turn started.");
            manager.CurrentGameState = GameState.EnemyTurn;
            gameManager.enemy.GainMana();
            CoroutineRunner.Instance.StartCoroutine(EnemyAction(manager, player, enemy, gameManager));
        }

        public override void UpdateState(TurnManager manager, Player player, Enemy enemy, GameManager gameManager = null) { }

        public override void ExitState(TurnManager manager, Player player, Enemy enemy, GameManager gameManager = null)
        {
            Debug.Log("Enemy's turn ended.");
            gameManager.enemy.UpdateEffects();
            isPlaying = false;
        }

        private IEnumerator EnemyAction(TurnManager manager, Player player, Enemy enemy, GameManager gameManager = null)
        {
            yield return new WaitForSeconds(manager.turnDuration);

            gameManager.EnemyTurn();

            Debug.Log("Enemy Attacks!");
            manager.SwitchState(new PlayerTurnState());
        }
    }
}
