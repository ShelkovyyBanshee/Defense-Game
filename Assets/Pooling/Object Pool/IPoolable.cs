

namespace Pooling
{
    public interface IPoolable
    {
        public void InitAfterActivation();

        public void BackToPool();
    }
}