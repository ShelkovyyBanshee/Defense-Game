using Pausing;
using UnityEngine;

namespace DefenseGame
{ 
    public abstract class Level : MonoBehaviour
    {
        public PoolingSystem PoolingSystem => _poolingSystem;

        protected Fabric Fabric => _fabric;
        protected PausingSystem PausingSystem => _pausingSystem;
        [Header("Job System")]
        [SerializeField] float _jobListUpdateTimeInterval;

        [Header("Pooling")]
        [SerializeField] int _basePoolSize;

        private Fabric _fabric;

        private PausingSystem _pausingSystem;
        private PoolingSystem _poolingSystem;
        private BattleFieldSystem _battleFieldSystem;
        private BulletLaunchingSystem _bulletLaunchingSystem;
        private JobSystem _jobSystem;
        

        private IPlayerCharacter _playerCharacter;
        private EnemyCounter _enemyCounter;

        private static Level _instance;

        public virtual void Initialize()
        {
            _fabric = new Fabric();

            _enemyCounter = new EnemyCounter();

            InitializePausingSystem();
            InitializePools();

            _playerCharacter = CreatePlayerCharacter();

            InitializeBattleFieldSystem();
            InitializeBulletLaunchingSystem();
            InitializeJobSystem();

            InitializeFabric();
        }

        public virtual void StartLevel()
        {
            var directoryModifier = new BulletDirectoryModifier(_battleFieldSystem.BulletsDirectory);
            _bulletLaunchingSystem.AddLaunchingModifier(directoryModifier);
        }

        public void RestartLevel()
        {
            PrepareBeforeRestart();
            StartLevel();
        }

        public virtual void CloseLevel()
        {
            _pausingSystem.Pause();
            _poolingSystem.ReturnAllObjectsBackToPools();
            _bulletLaunchingSystem.OnLevelClose();
            _jobSystem.ClearJobs();
        }

        protected virtual void PrepareBeforeRestart()
        {
            PausingSystem.Continue();
        }

        protected virtual void InitializeFabric()
        {
            _fabric.AddGeneralModifier(new PausingSystemInjector(_pausingSystem.Provider));
            _fabric.AddGeneralModifier(new PoolingSystemInjector(_poolingSystem));
            _fabric.AddGeneralModifier(new BattleFieldSystemInjector(_battleFieldSystem));

            _fabric.AddSpecialModifier("Enemy", new EnemyCounterInjector(_enemyCounter));

            _fabric.AddSpecialModifier("Weapon",
                new BulletLaunchingSystemInjector(_bulletLaunchingSystem.Provider));
            _fabric.AddSpecialModifier("Weapon", new FullCharger());
        }

        private void InitializePools()
        {
            var emptyObj = new GameObject("[POOLING SYSTEM]");

            emptyObj.transform.SetParent(transform);
            _poolingSystem = emptyObj.AddComponent<PoolingSystem>();

            _poolingSystem.Initialize(_fabric, _basePoolSize);
        }

        private void InitializePausingSystem()
        {
            _pausingSystem = new PausingSystem();
        }

        private void InitializeBattleFieldSystem()
        {
            _battleFieldSystem = GameObject.FindGameObjectWithTag("Battle Field System")
                .GetComponent<BattleFieldSystem>();
        }

        private void InitializeBulletLaunchingSystem()
        {
            _bulletLaunchingSystem = new BulletLaunchingSystem();
        }

        private void InitializeJobSystem()
        {
            var emptyObj = new GameObject("[JOB SYSTEM]");
            emptyObj.transform.SetParent(transform);

            _jobSystem = emptyObj.AddComponent<JobSystem>();
            _jobSystem.SetListUpdateInterval(_jobListUpdateTimeInterval);
        }

        protected abstract IPlayerCharacter CreatePlayerCharacter();

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
                return;
            }

            _instance = this;
        }
    }
}
