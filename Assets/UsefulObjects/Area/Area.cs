using System;
using UnityEngine;

namespace UsefulObjects
{
    [Serializable]
    public class Area
    {
        public Vector2 position => _position;
        public float x => _position.x;
        public float y => _position.y;

        public float width => _sizes.Width;
        public float height => _sizes.Height;

        [SerializeField] private Vector2 _position;
        [SerializeField] private AreaSizes _sizes;

        public Area(Vector2 position, float width, float height)
        {
            _position = position;
            _sizes = new AreaSizes(width, height);
        }

        public Vector2 RandomPoint()
        {
            float randomX = UnityEngine.Random.Range(x - width / 2, x + width / 2);
            float randomY = UnityEngine.Random.Range(y - height / 2, y + height / 2);

            return new Vector2(randomX, randomY);
        }

        public Vector2 RandomPointFixedX(float x)
        {
            float randomY = UnityEngine.Random.Range(y - height / 2, y + height / 2);

            return new Vector2(x, randomY);
        }

        public Vector2 RandomPointFixedY(float y)
        {
            float randomX = UnityEngine.Random.Range(x - width / 2, x + width / 2);
            
            return new Vector2(randomX, y);
        }

        public void DrawGizmos(Color color)
        {
            Gizmos.color = color;
            Gizmos.matrix = Matrix4x4.TRS(new Vector2(0, 0), Quaternion.identity, new Vector2(1, 1));
            Gizmos.DrawCube(new Vector2(x, y), new Vector2(width, height));
        }

        [Serializable]
        public struct AreaSizes
        {
            public float Width => _width;
            public float Height => _height;

            [SerializeField] private float _width;
            [SerializeField] private float _height;

            public AreaSizes(float width, float height)
            {
                _width = width;
                _height = height;
            }
        }
    }
}