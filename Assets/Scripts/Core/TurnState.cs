
namespace Assets.Scripts.Core
{
    public abstract class TurnState
    {
        public abstract void EnterState(TurnManager manager);
        public abstract void UpdateState(TurnManager manager);
        public abstract void ExitState(TurnManager manager);
    }
}