using UnityEngine;


namespace DefenseGame
{
    public class SingleEnemySpawner : MonoBehaviour
    {
        [SerializeField] private KeyCode _enemySpawnKey;
        [SerializeField] private int _basePoolVolume;
        [SerializeField] private EnemyComponent _enemyPrefab;
        [SerializeField] private Transform directoryAlive;

        private PoolingSystem _pool;
        private Transform _transform;

        public void Initialize(PoolingSystem pool)
        {
            _pool = pool;
        }

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(_enemySpawnKey))
            {
                var enemy = _pool.Get(_enemyPrefab);
                enemy.Transform.SetParent(directoryAlive);
                enemy.InstantlyMovePosition(_transform.position);
            }
        }
    }
}