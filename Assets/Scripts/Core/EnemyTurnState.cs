using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Core
{
    public class EnemyTurnState : TurnState
    {
        public override void EnterState(TurnManager manager)
        {
            Debug.Log("Enemy's turn started.");
            manager.CurrentGameState = GameState.EnemyTurn;
            manager.StartCoroutine(EnemyAction(manager));
        }

        public override void UpdateState(TurnManager manager) { }

        public override void ExitState(TurnManager manager)
        {
            Debug.Log("Enemy's turn ended.");
        }

        private IEnumerator EnemyAction(TurnManager manager)
        {
            yield return new WaitForSeconds(2f); // Düşman hamlesi yapıyormuş gibi bekletme
            manager.SwitchState(new PlayerTurnState());
        }
    }
}