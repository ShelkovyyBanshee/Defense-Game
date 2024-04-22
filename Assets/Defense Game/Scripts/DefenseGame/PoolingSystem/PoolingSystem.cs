using Pooling;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

namespace DefenseGame
{
    public class PoolingSystem : MonoBehaviour
    {
        private Dictionary<PoolableComponent, ObjectPool<PoolableComponent>> _pools;

        private Dictionary<PoolableComponent, Transform> _directoriesOfPools;
        private Dictionary<string, Transform> _directoriesByTag;
        private Transform _mainDirectory;
        
        private Fabric _fabric;

        private int _basePoolSize;

        public void Initialize(Fabric fabric, int basePoolSize)
        {
            _fabric = fabric;
            _basePoolSize = basePoolSize;
        }

        public T Get<T>(T prefab) where T : PoolableComponent
        {
            if (!_pools.ContainsKey(prefab))
            {
                AddPool(prefab);
            }

            var objToReturn = (T)_pools[prefab].Get();
            objToReturn.transform.SetParent(_directoriesOfPools[prefab]);

            return objToReturn;
        }

        public void AddPool<T>(T prefab) where T : PoolableComponent
        {
            var parentObj = new GameObject($"Object Pool of: {prefab.name}");
            var directory = parentObj.transform;
            AddToTagDirectory(prefab.tag, directory);
            _directoriesOfPools[prefab] = directory;

            var pool = new ObjectPool<PoolableComponent>(prefab, _fabric, 0);
            pool.onObjectWasCreated += OnObjectWasCreated;
            pool.Extend(_basePoolSize);
            
            _pools[prefab] = pool;
        }

        public void ReturnAllObjectsBackToPools()
        {
            foreach (var pool in _pools.Values)
            {
                pool.ReturnAllObjectsBack();
            }
        }

        private void AddToTagDirectory(string tag, Transform poolsDirectory)
        {
            if (!_directoriesByTag.ContainsKey(tag))
            {
                var emptyObj = new GameObject($"[{tag.ToUpper()}]");
                var directory = emptyObj.transform;
                directory.SetParent(_mainDirectory);
                _directoriesByTag[tag] = directory;
                poolsDirectory.transform.SetParent(directory);
            }
            else
            {
                poolsDirectory.transform.SetParent(_directoriesByTag[tag]);
            }
        }

        private void OnObjectWasCreated(PoolableComponent obj, PoolableComponent prefab)
        {
            var directoryInPool = _directoriesOfPools[prefab];
            obj.transform.SetParent(directoryInPool);
            obj.SetPoolDirectory(directoryInPool);
        }

        private void Awake()
        {
            _pools = new Dictionary<PoolableComponent, ObjectPool<PoolableComponent>>();
            _directoriesOfPools = new Dictionary<PoolableComponent, Transform>();
            _directoriesByTag = new Dictionary<string, Transform>();

            _mainDirectory = transform;
        }

        private void OnDisable()
        {
            if (_pools.Values == null) return;

            foreach(var pool in _pools.Values)
            {
                pool.onObjectWasCreated -= OnObjectWasCreated;
            }
        }
    }
}