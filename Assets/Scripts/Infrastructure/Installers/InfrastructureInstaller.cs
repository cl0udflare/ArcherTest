using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.CoroutineRunner;
using Infrastructure.Services.Factory;
using Infrastructure.Services.Factory.Arrow;
using Infrastructure.Services.Factory.Curtain;
using Infrastructure.Services.Factory.Hero;
using Infrastructure.Services.Factory.Hud;
using Infrastructure.Services.Factory.Popup;
using Infrastructure.Services.Factory.Target;
using Infrastructure.Services.SceneManagement;
using Infrastructure.Services.StaticData;
using Zenject;

namespace Infrastructure.Installers
{
    public class InfrastructureInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ICoroutineRunner>().To<CoroutineRunnerService>().FromComponentInHierarchy().AsSingle();
            
            BindServices();
            BindFactories();
            
            print("INFRASTRUCTURE INSTALLER INIT");
        }

        private void BindServices()
        {
            Container.Bind<SceneLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle(); 
            Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle(); 
        }

        private void BindFactories()
        {
            Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<CurtainFactory>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<HudFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<HeroFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<TargetFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<ArrowFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<TextPopupFactory>().AsSingle();
        }
    }
}