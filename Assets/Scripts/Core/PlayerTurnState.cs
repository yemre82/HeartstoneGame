using UnityEngine;
using Assets.Scripts.Players;

namespace Assets.Scripts.Core
{
    public class PlayerTurnState : TurnState
    {
        public override void EnterState(TurnManager manager, Player player, Enemy enemy, GameManager gameManager = null)
        {
            Debug.Log("Player's turn started.");
            manager.CurrentGameState = GameState.PlayerTurn;
            manager.IsPlayerDone = false;
            player.canPlay = true;
            gameManager.player.GainMana();
        }

        public override void UpdateState(TurnManager manager, Player player, Enemy enemy, GameManager gameManager = null)
        {
            if (manager.IsPlayerDone)
            {
                Debug.Log("Player made a move! Switching to Enemy Turn...");
                manager.SwitchState(new EnemyTurnState());
                player.canPlay = false;
            }
        }

        public override void ExitState(TurnManager manager, Player player, Enemy enemy, GameManager gameManager = null)
        {
            Debug.Log("Player's turn ended.");
            gameManager.player.UpdateEffects();
            player.canPlay = false;
        }
    }
}
