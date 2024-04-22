using Pausing;
using UnityEngine;
using UsefulObjects;


namespace DefenseGame
{
    public abstract class PauseController : MonoBehaviour, IPauseController, IActivationAffected
    {
        public InitOrder InitAfterActivationOrder => InitOrder.PauseControllerOrder;

        protected PausingSystemProvider Provider => _provider;

        private PausingSystemProvider _provider;
        private TrueFlagService _someoneNeedsPauseOn;
        private bool _isOnPause;

        private bool MustPause
        {
            get
            {
                return _provider.IsPauseOn || _someoneNeedsPauseOn.Flag;
            }
        }

        public void InitializeAfterActivation()
        {
            Validate();
        }

        public void AddPauseRequest()
        {
            _someoneNeedsPauseOn.AddTrueRequest();
            Validate();
        }

        public void RemovePauseRequest()
        {
            _someoneNeedsPauseOn.RemoveTrueRequest();
            Validate();
        }

        public void InitPauseComponent(PausingSystemProvider provider)
        {
            _provider = provider;
        }

        protected abstract void ExecuteOnPause();

        protected abstract void ExecuteOnContinue();

        protected abstract void AwakeAdditional();

        private void Validate()
        {
            if (_isOnPause != MustPause)
            {
                _isOnPause = MustPause;
                if (MustPause)
                    ExecuteOnPause();
                else
                    ExecuteOnContinue();
            }
        }

        private void Awake()
        {
            _someoneNeedsPauseOn = new TrueFlagService();
            AwakeAdditional();
        }

        private void OnEnable()
        {
            _isOnPause = false;
            if (_provider != null)
            {
                _provider.onPauseFlagChanged += Validate;
            }
        }

        private void OnDisable()
        {
            if (_provider != null)
            {
                _provider.onPauseFlagChanged -= Validate;
            }

            _someoneNeedsPauseOn.DeleteRequestInfo();
        }
    }
}