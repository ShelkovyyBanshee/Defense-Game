using System;
using UnityEngine;



namespace UnityExpansions
{
    public static class ColliderUtils
    {
        private static float DELTA = 0.2f;

        public static bool AreCollidersTouching(BoxCollider2D first, BoxCollider2D second)
        {
            var firstModel = new BoxCollider2DModel(first);
            var secondModel = new BoxCollider2DModel(second);

            return firstModel.IsTouching(secondModel, DELTA);
        }

        public struct BoxCollider2DModel
        {
            private Vector2 _rightUpper;
            private Vector2 _leftLower;

            public BoxCollider2DModel(BoxCollider2D boxCollider2D)
            {
                var size = boxCollider2D.size;
                var center = (Vector2)boxCollider2D.transform.position + boxCollider2D.offset;

                _rightUpper = new Vector2(center.x + size.x / 2, center.y + size.y / 2);
                _leftLower = new Vector2(center.x - size.x / 2, center.y - size.y / 2);
            }

            public bool IsTouching(BoxCollider2DModel other, float d=0.0f)
            {
                return ContainsPointOf(other, d) || other.ContainsPointOf(this, d);
            }

            private bool ContainsPointOf(BoxCollider2DModel other, float d=0.0f)
            {
                return ContainsPoint(other._rightUpper, d) || ContainsPoint(other._leftLower, d) ||
                    ContainsPoint(new Vector2(other._rightUpper.x, _leftLower.y), d) ||
                    ContainsPoint(new Vector2(other._leftLower.x, _rightUpper.y), d);
            }

            private bool ContainsPoint(Vector2 point, float d = 0.0f)
            {
                return (point.x <= _rightUpper.x + d && point.x >= _leftLower.x - d) &&
                    (point.y <= _rightUpper.y + d && point.y >= _leftLower.y - d);
            }

            public override string ToString()
            {
                return $"RightUpper({_rightUpper.x}, {_rightUpper.y}) " +
                    $"LeftLower({_leftLower.x}, {_leftLower.y})";
            }
        }
    }
}
