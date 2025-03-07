using Assets.Scripts.Players;
namespace Assets.Scripts.Core
{
    public abstract class TurnState
    {
        public abstract void EnterState(TurnManager manager, Player player, Enemy enemy);
        public abstract void UpdateState(TurnManager manager, Player player, Enemy enemy);
        public abstract void ExitState(TurnManager manager, Player player, Enemy enemy);
    }
}