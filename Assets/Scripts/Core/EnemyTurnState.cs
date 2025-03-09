using UnityEngine;
using System.Collections;
using Assets.Scripts.Players;

namespace Assets.Scripts.Core
{
    public class EnemyTurnState : TurnState
    {
        private bool isPlaying = false; // Çift çağrıyı önlemek için

        public override void EnterState(TurnManager manager, Player player, Enemy enemy, GameManager gameManager = null)
        {
            if (isPlaying) return; // Eğer zaten oynanıyorsa bir daha başlatma
            isPlaying = true;

            Debug.Log("Enemy's turn started.");
            manager.CurrentGameState = GameState.EnemyTurn;
            CoroutineRunner.Instance.StartCoroutine(EnemyAction(manager, player, enemy, gameManager));
        }

        public override void UpdateState(TurnManager manager, Player player, Enemy enemy, GameManager gameManager = null) { }

        public override void ExitState(TurnManager manager, Player player, Enemy enemy, GameManager gameManager = null)
        {
            Debug.Log("Enemy's turn ended.");
            isPlaying = false; // Yeni turn başladığında tekrar oynamasına izin ver
        }

        private IEnumerator EnemyAction(TurnManager manager, Player player, Enemy enemy, GameManager gameManager = null)
        {
            yield return new WaitForSeconds(manager.turnDuration);

            gameManager.EnemyTurn(); // GameManager içinde bir kez çağrılacak

            Debug.Log("Enemy Attacks!");
            manager.SwitchState(new PlayerTurnState());
        }
    }
}
