using DefenseGame.Testing;
using Unity.VisualScripting;
using UnityEngine;


namespace DefenseGame.Testing
{
    public class PauseByButtonInjector : IFabricModifier
    {
        public void ModifyObject(MonoBehaviour obj)
        {
            obj.AddComponent<TurnPauseOnByButtonComponent>();
        }
    }
}