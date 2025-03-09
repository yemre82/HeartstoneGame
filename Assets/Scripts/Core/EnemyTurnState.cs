using UnityEngine;
using System.Collections;
using Assets.Scripts.Players;

namespace Assets.Scripts.Core
{
    public class EnemyTurnState : TurnState
    {
        public override void EnterState(TurnManager manager, Player player, Enemy enemy, GameManager gameManager = null)
        {
            Debug.Log("Enemy's turn started.");
            manager.CurrentGameState = GameState.EnemyTurn;
            CoroutineRunner.Instance.StartCoroutine(EnemyAction(manager, player, enemy, gameManager));
        }

        public override void UpdateState(TurnManager manager, Player player, Enemy enemy, GameManager gameManager = null) { }

        public override void ExitState(TurnManager manager, Player player, Enemy enemy, GameManager gameManager = null)
        {
            Debug.Log("Enemy's turn ended.");
        }

        private IEnumerator EnemyAction(TurnManager manager, Player player, Enemy enemy, GameManager gameManager = null)
        {
            gameManager.EnemyTurn();
            yield return new WaitForSeconds(manager.turnDuration);

            Debug.Log("Enemy Attacks!");
            manager.SwitchState(new PlayerTurnState());
        }
    }
}
