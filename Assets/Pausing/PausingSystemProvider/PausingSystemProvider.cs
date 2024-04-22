using System;

namespace Pausing
{
    public class PausingSystemProvider
    {
        public event Action onPauseFlagChanged;

        public bool IsPauseOn => _pausingSystem.IsPauseOn;
        
        private PausingSystem _pausingSystem;

        public PausingSystemProvider(PausingSystem pausingSystem)
        {
            _pausingSystem = pausingSystem;
            _pausingSystem.onClose += OnClose;
            _pausingSystem.onPauseFlagChanged += OnPause;
        }

        private void OnPause()
        {
            onPauseFlagChanged?.Invoke();
        }

        private void OnClose()
        {
            _pausingSystem.onClose -= OnClose;
            _pausingSystem.onPauseFlagChanged -= OnPause;
        }
    }
}