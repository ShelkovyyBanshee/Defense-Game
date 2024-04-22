using Pooling;
using UnityEngine;


namespace DefenseGame
{
    public abstract class PoolableComponent : MonoBehaviour, IPoolable
    {
        public Transform Transform => _transform;

        private Transform _directoryInPool;
        private Transform _transform;
        private GameObject _gameObject;

        public abstract void InitAfterActivation();

        public void SetPoolDirectory(Transform directory)
        {
            _directoryInPool = directory;
        }

        public void BackToPool()
        {
            _transform.SetParent(_directoryInPool);
            _gameObject.SetActive(false);
        }

        protected virtual void Awake()
        {
            _transform = transform;
            _gameObject = gameObject;
        }
    }
}