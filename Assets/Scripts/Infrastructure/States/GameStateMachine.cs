using System;
using System.Collections.Generic;
using Infrastructure.Services.Factory;
using Zenject;

namespace Infrastructure.States
{
    public class GameStateMachine : IInitializable
    {
        private readonly StateFactory _stateFactory;
        
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(StateFactory stateFactory) => 
            _stateFactory = stateFactory;

        public void Initialize()
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)]    = _stateFactory.CreateState<BootstrapState>(),
                [typeof(LoadLevelState)]    = _stateFactory.CreateState<LoadLevelState>(),
                [typeof(GameLoopState)]     = _stateFactory.CreateState<GameLoopState>()
            };
            
            Enter<BootstrapState>();
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}