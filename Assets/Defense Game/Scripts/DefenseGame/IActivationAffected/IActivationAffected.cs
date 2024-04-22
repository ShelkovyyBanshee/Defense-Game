

namespace DefenseGame
{
    public interface IActivationAffected
    {
        public InitOrder InitAfterActivationOrder { get; }

        public void InitializeAfterActivation();
    }
}