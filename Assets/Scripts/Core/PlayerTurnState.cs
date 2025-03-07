using UnityEngine;
using Assets.Scripts.Players;

namespace Assets.Scripts.Core
{
    public class PlayerTurnState : TurnState
    {
        public override void EnterState(TurnManager manager, Player player, Enemy enemy)
        {
            Debug.Log("Player's turn started.");
            manager.CurrentGameState = GameState.PlayerTurn;
            manager.IsPlayerDone = false; // Yeni tur başladığında sıfırla
            player.canPlay = true;
        }

        public override void UpdateState(TurnManager manager, Player player, Enemy enemy)
        {
            if (manager.IsPlayerDone) // Eğer oyuncu hamle yaptıysa
            {
                Debug.Log("Player made a move! Switching to Enemy Turn...");
                manager.SwitchState(new EnemyTurnState());
                player.canPlay = false;
            }
        }

        public override void ExitState(TurnManager manager, Player player, Enemy enemy)
        {
            Debug.Log("Player's turn ended.");
            player.canPlay = false;
        }
    }
}
