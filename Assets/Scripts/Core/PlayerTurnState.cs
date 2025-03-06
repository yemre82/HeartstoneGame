using UnityEngine;

public class PlayerTurnState : TurnState
{
    public override void EnterState(TurnManager manager)
    {
        Debug.Log("Player's turn started.");
        manager.CurrentGameState = GameState.PlayerTurn;
    }

    public override void UpdateState(TurnManager manager)
    {
        // Oyuncunun kart oynayıp oynamadığını kontrol edebilirsin
        if (manager.IsPlayerDone)
        {
            manager.SwitchState(new EnemyTurnState());
        }
    }

    public override void ExitState(TurnManager manager)
    {
        Debug.Log("Player's turn ended.");
    }
}
