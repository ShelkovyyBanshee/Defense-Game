

namespace DefenseGame
{
    public class JobSystemProvider
    {
        private JobSystem _jobSystem;

        public JobSystemProvider(JobSystem jobSystem)
        {
            _jobSystem = jobSystem;
        }

        public void AddJob(IJob job)
        {
            _jobSystem.AddJob(job);
        }
    }
}