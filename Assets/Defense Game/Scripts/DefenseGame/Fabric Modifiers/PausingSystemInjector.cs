using UnityEngine;
using Pausing;


namespace DefenseGame
{
    public class PausingSystemInjector : IFabricModifier
    {
        private PausingSystemProvider _provider;

        public PausingSystemInjector(PausingSystemProvider provider)
        {
            _provider = provider;
        }

        public void ModifyObject(MonoBehaviour obj)
        {
            var pauseComponent = obj.GetComponent<IPauseController>();

            if (pauseComponent != null)
                pauseComponent.InitPauseComponent(_provider);
        }
    }
}