using UnityEngine;
using UsefulObjects;

namespace DefenseGame
{
    public class BattleFieldSystem : MonoBehaviour
    {
        public Area BattleArea => _battleArea;
        public Area SpawnArea => _spawnArea;

        public string BattleAreaTag => _battleAreaTag;
        public string BACollidersTag => _BACollidersTag;
        public string BACollidersLayer => _BACollidersLayer;

        public BoxCollider2D BattleAreaCollider => _battleAreaCollider;

        public Transform BulletsDirectory => _bulletsDirectory;
         
        [Header("Editor")]
        [SerializeField] bool _drawOnGizmos;

        [Space(25)]

        [Header("Battle Area")]
        [SerializeField] private Area _battleArea;
        [SerializeField] private string _battleAreaTag;
        [SerializeField] private string _battleAreaLayer;

        [SerializeField] private string _BACollidersLayer;
        [SerializeField] private string _BACollidersTag;
        [SerializeField] private Color _battleAreaColor;

        [Space(25)]

        [Header("SpawnArea")]
        [SerializeField] private Area _spawnArea;
        [SerializeField] private Color _spawnAreaColor;
        [Range(0.0f, 100.0f)] [SerializeField] private float _percentOfBattleAreaHeight;

        [Space(25)]
       
        [SerializeField] private float playersX;

        private BoxCollider2D _battleAreaCollider;
        private Transform _bulletsDirectory;

        private void CreateBattleArea()
        {
            var emptyObject = new GameObject("[BATTLE AREA COLLIDER]");

            emptyObject.gameObject.tag = _battleAreaTag;
            emptyObject.transform.SetParent(transform);
            emptyObject.layer = LayerMask.NameToLayer(_battleAreaLayer);

            emptyObject.AddComponent<BattleArea>();

            _battleAreaCollider = emptyObject.AddComponent<BoxCollider2D>();
            _battleAreaCollider.isTrigger = true;
            _battleAreaCollider.size = new Vector2(_battleArea.width, _battleArea.height);
            _battleAreaCollider.transform.position = new Vector2(_battleArea.x, _battleArea.y);
        }

        private void CreateDirectories()
        {
            var emptyObj = new GameObject("[BULLETS IN BATTLE]");
            _bulletsDirectory = emptyObj.transform;
            _bulletsDirectory.SetParent(transform);
        }

        private void Awake()
        {
            CreateBattleArea();
            CreateDirectories();
        }

        private void OnDrawGizmos()
        {
            if (_drawOnGizmos)
            {
                _battleArea.DrawGizmos(_battleAreaColor);
                _spawnArea.DrawGizmos(_spawnAreaColor);
            }
        }

        private void OnValidate()
        {
            _spawnArea = new Area(new Vector2(_spawnArea.x, _battleArea.y), 
                _spawnArea.width, _battleArea.height * _percentOfBattleAreaHeight / 100);
        }
    }
}