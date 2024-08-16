namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public GameLoopState(GameStateMachine stateMachine)
        {
        }

        public void Exit()
        {
        }

        public void Enter()
        {
        }
    }
}