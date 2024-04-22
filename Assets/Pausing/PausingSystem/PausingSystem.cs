using System;

namespace Pausing
{
    public class PausingSystem
    {
        public event Action onClose;
        public event Action onPauseFlagChanged;

        public bool IsPauseOn => _isOnPause;
        public PausingSystemProvider Provider => _provider;

        private bool _isOnPause;
        private PausingSystemProvider _provider;
        
        public PausingSystem()
        {
            _provider = new PausingSystemProvider(this);
        }

        public void Pause()
        {
            bool wasOnPause = _isOnPause;

            _isOnPause = true;

            if (!wasOnPause) onPauseFlagChanged.Invoke();
        }

        public void Continue()
        {
            bool wasOnPause = _isOnPause;

            _isOnPause = false;

            if (wasOnPause) onPauseFlagChanged.Invoke();
        }

        public void Close()
        {
            onClose.Invoke();
            _provider = null;
        }
    }
}