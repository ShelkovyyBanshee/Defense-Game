using System.Linq;


namespace DefenseGame
{
    public class ComplexInitPoolableComponent : PoolableComponent
    {
        public IActivationAffected[] ActivationAffected => _activationAffected;

        private IActivationAffected[] _activationAffected;

        public override void InitAfterActivation()
        {
            if (_activationAffected != null)
            {
                foreach (var component in ActivationAffected)
                {
                    component.InitializeAfterActivation();
                }
            }

            ExecuteAfterActivationAdditional();
        }

        protected virtual void ExecuteAfterActivationAdditional() { }
        protected virtual void AwakeAdditional() { }

        protected override void Awake()
        {
            base.Awake();

            var componentsFound = GetComponents<IActivationAffected>();
            if (componentsFound.Length != 0)
            {
                _activationAffected = componentsFound.OrderBy(x => (int)x.InitAfterActivationOrder).ToArray();
            }

            AwakeAdditional();
        }
    }
}