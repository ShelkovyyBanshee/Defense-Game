using UnityEngine;


namespace DefenseGame
{
    public interface IJob
    {
        public bool IsTimeForJob { get; }
        public bool WasCanceled { get; }
        public bool WasDone { get; }

        public void Do();

        public void ProgressTime();
    }
}