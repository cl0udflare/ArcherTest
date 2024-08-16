using Infrastructure.Services.SceneManagement;
using Infrastructure.Services.StaticData;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IStaticDataService _staticData;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, IStaticDataService staticData)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _staticData = staticData;
        }

        public void Enter()
        {
            _staticData.LoadStaticData();
            _sceneLoader.Load(SceneName.Initial.ToSceneString(), EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadLevelState, string>(SceneName.Main.ToSceneString());
    }
}