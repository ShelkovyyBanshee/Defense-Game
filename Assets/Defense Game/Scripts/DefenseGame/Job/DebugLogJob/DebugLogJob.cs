


using UnityEngine;

namespace DefenseGame
{
    public class DebugLogJob : Job
    {
        private string _text;

        public DebugLogJob(float waitTime, string text) : base(waitTime)
        {
            _text = text;
        }

        public override void Do()
        {
            base.Do();
            Debug.Log(_text);
        }
    }
}