using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;


namespace DefenseGame
{
    [Serializable]
    public class FloatRandomizer
    {
        [SerializeField] List<Section> _sections;

        public float GetRandomFloat()
        {
            var sumWeight = GetSumWeight();

            var checkValue = UnityEngine.Random.Range(0.0f, sumWeight);
            var temp = 0.0f;
            

            foreach(var section in _sections.OrderBy(s => -s.Weight - 1))
            {
                temp += section.Weight;
                if (checkValue <= temp)
                    return section.Value;
            }

            return 0;
        }

        public float GetSumWeight()
        {
            float result = 0;

            foreach (var section in _sections)
                result += section.Weight;

            return result;
        }

        [Serializable]
        public struct Section
        {
            public float Value
            {
                get
                {
                    return _value;
                }
                set
                {
                    _value = value;
                }
            }

            public float Weight
            {
                get
                {
                    return _weight;
                }
                set
                {
                    _weight = value;
                }
            }

            [SerializeField] float _value;
            [SerializeField] float _weight;

            public Section(float value, float weight)
            {
                _value = value;
                _weight = weight;
            }
        }
    }
}