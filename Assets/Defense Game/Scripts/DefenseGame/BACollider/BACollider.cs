using System;
using UnityEngine;
using UnityExpansions;


namespace DefenseGame 
{
    public class BACollider : MonoBehaviour
    {
        public event Action onEnterBattleArea;
        public event Action onExitBattleArea;

        public bool IsOnBattleAreaEnterExitVar => _collider.IsTouching(_battleAreaCollider);
        public bool IsOnBattleAreaInstantVar => ColliderUtils.AreCollidersTouching(_collider, _battleAreaCollider);
        public BoxCollider2D Collider => _collider;

        private BoxCollider2D _collider;
        private BoxCollider2D _battleAreaCollider;

        public void Disable()
        {
            _collider.enabled = false;
        }

        public void Initialize(BoxCollider2D referenceCollider, BattleFieldSystem battleField)
        {
            var gameObj = gameObject;

            _collider.size = referenceCollider.size;
            _collider.offset = referenceCollider.offset;
            transform.position = referenceCollider.transform.position;

            _battleAreaCollider = battleField.BattleAreaCollider;

            gameObj.tag = battleField.BACollidersTag;
            gameObj.layer = LayerMask.NameToLayer(battleField.BACollidersLayer);
        }

        private void Awake()
        {
            _collider = gameObject.AddComponent<BoxCollider2D>();
        }

        public void OnEnterBattleArea()
        {
            onEnterBattleArea?.Invoke();
        }

        public void OnExitBattleArea()
        {
            onExitBattleArea?.Invoke();
        }

        private void OnEnable()
        {
            _collider.enabled = true;
        }
    }
}