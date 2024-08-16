using Infrastructure.Services.Factory.Arrow;
using Infrastructure.Services.Factory.Curtain;
using Infrastructure.Services.Factory.Hero;
using Infrastructure.Services.Factory.Hud;
using Infrastructure.Services.Factory.Popup;
using Infrastructure.Services.Factory.Target;
using Infrastructure.Services.SceneManagement;
using Logic;
using Logic.ArcherLogic;
using UI.Hud;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ICurtainFactory _curtainFactory;
        private readonly IHudFactory _hudFactory;
        private readonly IHeroFactory _heroFactory;
        private readonly ITargetFactory _targetFactory;
        private readonly IArrowFactory _arrowFactory;
        private readonly ITextPopupFactory _popupFactory;
        
        private Archer _archer;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, ICurtainFactory curtainFactory,
            IHudFactory hudFactory, IHeroFactory heroFactory, ITargetFactory targetFactory, IArrowFactory arrowFactory,
            ITextPopupFactory popupFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtainFactory = curtainFactory;
            _hudFactory = hudFactory;
            _heroFactory = heroFactory;
            _targetFactory = targetFactory;
            _arrowFactory = arrowFactory;
            _popupFactory = popupFactory;
        }

        public void Enter(string sceneName)
        {
            _curtainFactory.Curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _curtainFactory.Curtain.Hide();
            _archer.AimToTarget();
        }

        private void OnLoaded()
        {
            InitGameWorld();
            _stateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            Aim target = InitTarget();
            _archer = InitHero(target);
            InitHud(target);
        }

        private void InitHud(Aim target)
        {
            HudComponents hud = _hudFactory.CreateHud();

            hud.SpawnArrow.RegisterCallback(() =>
            {
                target.MoveTargetToRandomPosition();
                _archer.AimToTarget();
            });

            hud.ToggleFire.ToggleRegister(state =>
            {
                _archer.TakeFireArrow = state;
            });
        }

        private Aim InitTarget()
        {
            Aim target = _targetFactory.CreateTarget();
            target.Construct(_popupFactory);

            return target;
        }

        private Archer InitHero(Aim target)
        {
            Archer archer = _heroFactory.CreateHero();
            archer.Construct(target, _arrowFactory, _popupFactory);

            return archer;
        }
    }
}