using System;
using UnityEngine;
using UnityExpansions;


namespace DefenseGame
{
    public abstract class BattleAreaDetector : MonoBehaviour, IBattleFieldSystemUser
    {
        public event Action onEnterBattleArea;
        public event Action onExitBattleArea;

        public BACollider BACollider => _baCollider;
        public bool IsOnBattleAreaEnterExitVar => _baCollider.IsOnBattleAreaEnterExitVar;
        public bool IsOnBattleAreaInstantVar => _baCollider.IsOnBattleAreaInstantVar;

        private BACollider _baCollider;
        private BattleFieldSystem _battleField;

        public void SetBattleFieldSystem(BattleFieldSystem battleFieldSystem)
        {
            _battleField = battleFieldSystem;
        }

        protected virtual void OnEnterBattleArea()
        {
            onEnterBattleArea?.Invoke();
        }

        protected virtual void OnExitBattleArea()
        {
            onExitBattleArea?.Invoke();
        }

        protected virtual void Awake()
        {
            var emptyObj = new GameObject("BACollider");
            emptyObj.transform.SetParent(transform);

            _baCollider = emptyObj.AddComponent<BACollider>();
            _baCollider.Initialize(GetComponent<BoxCollider2D>(), _battleField);
        }

        protected virtual void OnEnable()
        {
            _baCollider.onEnterBattleArea += OnEnterBattleArea;
            _baCollider.onExitBattleArea += OnExitBattleArea;
        }

        protected virtual void OnDisable()
        {
            _baCollider.onEnterBattleArea -= OnEnterBattleArea;
            _baCollider.onExitBattleArea -= OnExitBattleArea;
        }
    }
}