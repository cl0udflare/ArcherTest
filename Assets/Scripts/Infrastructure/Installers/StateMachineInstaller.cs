using Infrastructure.States;
using Zenject;

namespace Infrastructure.Installers
{
    public class StateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<BootstrapState>().AsSingle().NonLazy();
            Container.Bind<LoadLevelState>().AsSingle().NonLazy();
            Container.Bind<GameLoopState>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle(); // Entry point
            
            print("STATE MACHINE INSTALLER INIT");
        }
    }
}