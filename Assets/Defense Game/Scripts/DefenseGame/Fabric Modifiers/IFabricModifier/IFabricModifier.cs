using UnityEngine;

namespace DefenseGame
{
    public interface IFabricModifier
    {
        public void ModifyObject(MonoBehaviour obj);
    }
}