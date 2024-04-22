using System.Collections.Generic;
using UnityEngine;

namespace Pooling
{
    public class ObjectPool<T> where T : MonoBehaviour, IPoolable
    {
        public delegate void OnObjectWasCreatedHandler(T obj, T prefab);

        public event OnObjectWasCreatedHandler onObjectWasCreated;

        private IFabric _fabric;
        private List<T> _objects;
        private T _prefab;

        public ObjectPool(T prefab, IFabric fabric, int minSize)
        {
            _fabric = fabric;
            _prefab = prefab;

            _objects = new List<T>();
            Extend(minSize);
        }

        public void ReturnAllObjectsBack()
        {
            foreach(var obj in _objects)
            {
                obj.BackToPool();
            }
        }

        public T Get()
        {
            T objectToReturn = null;

            foreach (var obj in _objects)
            {
                if (!obj.isActiveAndEnabled)
                {
                    obj.gameObject.SetActive(true);
                    
               
                    objectToReturn = obj;
                    break;
                }
            }
            
            if (objectToReturn == null)
                objectToReturn = Create();

            objectToReturn.InitAfterActivation();

            return objectToReturn;
        }

        public void Extend(int objAmount)
        {
            for (int i = 0; i < objAmount; i++)
            {
                var obj = Create();
                obj.gameObject.SetActive(false);
            }
        }

        private T Create()
        {
            var obj = _fabric.Create(_prefab);
            _objects.Add(obj);

            onObjectWasCreated?.Invoke(obj, _prefab);

            return obj;
        }

    }
}