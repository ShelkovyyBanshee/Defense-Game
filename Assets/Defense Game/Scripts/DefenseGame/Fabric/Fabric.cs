using UnityEngine;
using System.Collections.Generic;
using UnityExpansions;
using Pooling;


namespace DefenseGame
{
    public class Fabric : IFabric
    {
        private FabricModifiersGroup _modifiers;
        
        public T Create<T>(T prefab) where T : MonoBehaviour
        {
            T obj = InstantiationExpansion.InstantiateDisabled(prefab);

            _modifiers.Modify(obj);

            obj.gameObject.SetActive(true);

            return obj;
        }

        public void AddGeneralModifier(IFabricModifier injector)
        {
            _modifiers.AddGeneralModifier(injector);
        }

        public void AddSpecialModifier(string tag, IFabricModifier injector)
        {
            _modifiers.AddSpecialModifier(tag, injector);
        }

        public Fabric()
        {
            _modifiers = new FabricModifiersGroup();
        }

        private class FabricModifiersGroup
        {
            private List<IFabricModifier> _generalModifiers;
            private Dictionary<string, List<IFabricModifier>> _specialModifiers;
            
            public void Modify(MonoBehaviour obj)
            {
                foreach(var injector in _generalModifiers)
                {
                    injector.ModifyObject(obj);
                }

                string objTag = obj.gameObject.tag;

                if (_specialModifiers.ContainsKey(objTag))
                {
                    var modifiers = _specialModifiers[objTag];

                    foreach (var modifier in modifiers)
                    {
                        modifier.ModifyObject(obj);
                    }
                }
            }

            public void AddGeneralModifier(IFabricModifier modifier)
            {
                _generalModifiers.Add(modifier);
            }

            public void AddSpecialModifier(string tag, IFabricModifier modifier)
            {
                if (!_specialModifiers.ContainsKey(tag))
                    _specialModifiers[tag] = new List<IFabricModifier>();

                _specialModifiers[tag].Add(modifier);
            }

            public FabricModifiersGroup()
            {
                _generalModifiers = new List<IFabricModifier>();
                _specialModifiers = new Dictionary<string, List<IFabricModifier>>();
            }
        }
    }
}