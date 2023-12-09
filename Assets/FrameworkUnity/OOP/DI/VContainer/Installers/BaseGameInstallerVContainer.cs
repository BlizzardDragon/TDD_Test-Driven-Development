using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FrameworkUnity.OOP.VContainer.Installers
{
    public abstract class BaseGameInstallerVContainer : LifetimeScope
    {
        [Header("AutoRun")]
        [SerializeField] private bool _initGame = true;
        [SerializeField] private bool _prepareGame = true;
        [SerializeField] private bool _startGame = true;

        private GameManagerVContainer _gameManager;


        protected abstract void ConfigureSystems(IContainerBuilder builder);

        protected override void Configure(IContainerBuilder builder)
        {
            ConfigureSystems(builder);
            InstallGameManager(builder);
        }

        protected override void Awake()
        {
            base.Awake();

            if (_initGame)
            {
                _gameManager.InitGame();
            }
        }

        public void Start()
        {
            if (_prepareGame)
            {
                _gameManager.PrepareGame();
            }

            if (_startGame)
            {
                _gameManager.StartGame();
            }
        }

        private void InstallGameManager(IContainerBuilder builder)
        {
            _gameManager = GetComponent<GameManagerVContainer>();
            builder.RegisterComponent(_gameManager).As<GameManagerVContainer>();
        }
    }
}