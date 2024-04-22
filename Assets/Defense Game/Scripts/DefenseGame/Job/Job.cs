

using UnityEngine;

namespace DefenseGame
{
    public abstract class Job : IJob
    {
        public bool IsTimeForJob => _timePassed >= _waitTime;
        public bool WasCanceled => _wasCanceled;
        public bool WasDone => _wasDone;

        private float _timePassed;
        private float _waitTime;

        private bool _wasDone;
        private bool _wasCanceled;

        public Job(float waitTime)
        {
            _waitTime = waitTime;
            _timePassed = 0.0f;
            _wasDone = false;
            _wasCanceled = false;
        }

        public virtual void Do()
        {
            _wasDone = true;
        }

        public void ProgressTime()
        {
            _timePassed += Time.deltaTime;
        }
    }
}