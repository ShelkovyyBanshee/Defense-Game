using UnityEngine;

namespace Pooling
{
    public interface IFabric
    {
        public T Create<T>(T prefab) where T: MonoBehaviour;
    }
}