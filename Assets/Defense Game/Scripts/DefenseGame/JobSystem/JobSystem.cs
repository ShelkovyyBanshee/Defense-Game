using System.Collections.Generic;
using UnityEngine;


namespace DefenseGame
{
    public class JobSystem : MonoBehaviour
    {
        public JobSystemProvider Provider => _provider;

        private List<IJob> _jobs;
        private JobSystemProvider _provider;

        private float _listUpdateTimeInterval;
        private float _timeUntilListUpdatePassed;
        
        public void SetListUpdateInterval(float interval)
        {
            _listUpdateTimeInterval = interval;
        }

        public void AddJob(IJob job)
        {
            _jobs.Add(job);
        }

        public void ClearJobs()
        {
            _jobs = new List<IJob>();
        }

        private void UpdateJobList()
        {
            var newJobList = new List<IJob>();

            foreach(var job in _jobs)
            {
                if (!job.WasDone && !job.WasCanceled)
                    newJobList.Add(job);
            }

            _jobs = newJobList;
        }

        private void Update()
        {
            foreach (var job in _jobs)
            {
                if (!job.WasDone && !job.WasCanceled && job.IsTimeForJob)
                    job.Do();
                else
                    job.ProgressTime();
            }

            _timeUntilListUpdatePassed += Time.deltaTime;

            if (_timeUntilListUpdatePassed >= _listUpdateTimeInterval)
            {
                _timeUntilListUpdatePassed = 0.0f;
                UpdateJobList();
            }
        }

        private void Awake()
        {
            _jobs = new List<IJob>();
            _provider = new JobSystemProvider(this);
        }
    }
}