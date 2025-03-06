using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private TurnState currentState;

    [SerializeField] private GameState currentGameState;
    public GameState CurrentGameState {
        get => currentGameState;
        set => currentGameState = value;
    }
    public bool IsPlayerDone { get; set; }

    void Start()
    {
        CurrentGameState = GameState.NotStarted;
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
        if (currentState != null)
        {
            currentState.ExitState(this);
        }

        currentState = newState;
        CurrentGameState = (newState is PlayerTurnState) ? GameState.PlayerTurn :
                           (newState is EnemyTurnState) ? GameState.EnemyTurn :
                           GameState.NotStarted;

        currentState.EnterState(this);
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }
    }
}
