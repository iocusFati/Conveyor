using System;
using System.Collections.Generic;
using CameraBehaviour;
using GamePlay.FoodFolder.FoodSpawnerFolder.Pool;
using GamePlay.QuestFolder;
using Infrastructure.Bootstrapper;
using Infrastructure.Factories;
using Infrastructure.Services;
using Infrastructure.Services.StaticDataService;
using UI.Services;

namespace Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitState> _states;
        private IExitState _currentState;
        public bool IsInGameLoop => _currentState is GameLoopState; 


        public GameStateMachine(SceneLoader sceneLoader, AllServices services, ICoroutineRunner coroutineRunner,
            ITicker ticker)
        {
            var cameraMovement = new CameraMovement(coroutineRunner);

            _states = new Dictionary<Type, IExitState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services, coroutineRunner, ticker),
                [typeof(LoadLevelState)] = new LoadLevelState(
                    this , sceneLoader, services.Single<IPlayerFactory>(), services.Single<ILocationFactory>(),
                    services.Single<IUIFactory>(), services.Single<IQuestFactory>(), services.Single<IStaticDataService>(), cameraMovement),
                [typeof(GameLoopState)] = new GameLoopState(
                    services.Single<ILocationFactory>(), services.Single<IFoodPool>()),
                [typeof(GameWonState)] = new GameWonState(
                    services.Single<IFoodPool>(), services.Single<IUIFactory>(), services.Single<IPlayerFactory>(),
                    services.Single<ILocationFactory>() ,cameraMovement)
            };
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

        private TState ChangeState<TState>() where TState : class, IExitState
        {
            _currentState?.Exit();

            TState state = GetState<TState>();
            _currentState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitState => 
            _states[typeof(TState)] as TState;
    }
}