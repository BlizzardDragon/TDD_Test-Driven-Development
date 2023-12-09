using UnityEngine;
using System;
using FrameworkUnity.OOP.VContainer.Installers;
using FrameworkUnity.OOP.Interfaces.Listeners;
using System.Collections.Generic;
using VContainer;

namespace FrameworkUnity.OOP.VContainer
{
    [AddComponentMenu("GameManagers/GameManagerVContainer")]
    [RequireComponent(typeof(SceneLifetimeScope))]
    public sealed class GameManagerVContainer : BaseGameManager
    {
        private float _deltaTime;

        private readonly List<IGameListener> _listeners = new();
        private readonly List<IUpdateGameListener> _updateListeners = new();
        private readonly List<IFixedUpdateGameListener> _fixedUpdateListeners = new();
        private readonly List<ILateUpdateGameListener> _lateUpdateListeners = new();

        public event Action OnInitGame;
        public event Action OnDeInitGame;
        public event Action OnPrepareGame;
        public event Action OnStartGame;
        public event Action OnPauseGame;
        public event Action OnResumeGame;
        public event Action OnFinishGame;
        public event Action OnWinGame;
        public event Action OnLoseGame;

        [Inject]
        private readonly IObjectResolver _objectResolver;


        public void AddListener(IGameListener listener)
        {
            if (listener == null) return;

            _listeners.Add(listener);

            if (listener is IUpdateGameListener updateListener)
            {
                _updateListeners.Add(updateListener);
            }

            if (listener is IFixedUpdateGameListener fixedUpdateListener)
            {
                _fixedUpdateListeners.Add(fixedUpdateListener);
            }

            if (listener is ILateUpdateGameListener lateUpdateListener)
            {
                _lateUpdateListeners.Add(lateUpdateListener);
            }
        }

        public void RemoveListener(IGameListener listener)
        {
            if (listener == null) return;

            _listeners.Remove(listener);

            if (listener is IUpdateGameListener updateListener)
            {
                _updateListeners.Remove(updateListener);
            }

            if (listener is IFixedUpdateGameListener fixedUpdateListener)
            {
                _fixedUpdateListeners.Remove(fixedUpdateListener);
            }

            if (listener is ILateUpdateGameListener lateUpdateListener)
            {
                _lateUpdateListeners.Remove(lateUpdateListener);
            }
        }

        protected override void Update()
        {
            if (State != GameState.Play) return;

            float _deltaTime = Time.deltaTime;
            for (int i = 0; i < _updateListeners.Count; i++)
            {
                _updateListeners[i].OnUpdate(_deltaTime);
            }
        }

        protected override void FixedUpdate()
        {
            if (State != GameState.Play) return;

            for (int i = 0; i < _fixedUpdateListeners.Count; i++)
            {
                _fixedUpdateListeners[i].OnFixedUpdate(_fixedDeltaTime);
            }
        }

        protected override void LateUpdate()
        {
            if (State != GameState.Play) return;

            for (int i = 0; i < _lateUpdateListeners.Count; i++)
            {
                _lateUpdateListeners[i].OnLateUpdate(_deltaTime);
            }
        }

        internal override void InitGame()
        {
            foreach (var listener in _objectResolver.Resolve<IEnumerable<IGameListener>>())
            {
                AddListener(listener);
            }

            _fixedDeltaTime = Time.fixedDeltaTime;

            foreach (var listener in _listeners)
            {
                if (listener is IInitGameListener initListener)
                {
                    initListener.OnInitGame();
                }
            }

            OnInitGame?.Invoke();
        }

        internal override void DeInitGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IDeInitGameListener deInitListener)
                {
                    deInitListener.OnDeInitGame();
                }
            }

            OnDeInitGame?.Invoke();
        }

        public override void PrepareGame()
        {
            State = GameState.Prepare;

            foreach (var listener in _listeners)
            {
                if (listener is IPrepareGameListener prepareListener)
                {
                    prepareListener.OnPrepareGame();
                }
            }

            OnPrepareGame?.Invoke();
        }

        public override void StartGame()
        {
            Debug.Log("StartGame!");
            State = GameState.Play;

            foreach (var listener in _listeners)
            {
                if (listener is IStartGameListener startListener)
                {
                    startListener.OnStartGame();
                }
            }

            OnStartGame?.Invoke();
        }

        public override void PauseGame()
        {
            State = GameState.Pause;

            foreach (var listener in _listeners)
            {
                if (listener is IPauseGameListener pauseListener)
                {
                    pauseListener.OnPauseGame();
                }
            }

            OnPauseGame?.Invoke();
        }

        public override void ResumeGame()
        {
            State = GameState.Play;

            foreach (var listener in _listeners)
            {
                if (listener is IResumeGameListener resumeListener)
                {
                    resumeListener.OnResumeGame();
                }
            }

            OnResumeGame?.Invoke();
        }

        public override void WinGame()
        {
            State = GameState.Win;

            foreach (var listener in _listeners)
            {
                if (listener is IWinGameListener gameWinListener)
                {
                    gameWinListener.OnWinGame();
                }
            }

            OnWinGame?.Invoke();
            DeInitGame();
        }

        public override void LoseGame()
        {
            State = GameState.Lose;

            foreach (var listener in _listeners)
            {
                if (listener is ILoseGameListener gameOverListener)
                {
                    gameOverListener.OnLoseGame();
                }
            }

            OnLoseGame?.Invoke();
            DeInitGame();
        }

        public override void FinishGame()
        {
            State = GameState.Finish;

            foreach (var listener in _listeners)
            {
                if (listener is IFinishGameListener finishListener)
                {
                    finishListener.OnFinishGame();
                }
            }

            OnFinishGame?.Invoke();
            DeInitGame();
        }
    }
}