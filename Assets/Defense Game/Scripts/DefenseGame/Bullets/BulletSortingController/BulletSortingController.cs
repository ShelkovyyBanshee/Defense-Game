using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;


namespace DefenseGame
{
    public class BulletSortingController : MonoBehaviour
    {
        public bool ShowOverWeapon => _showOverWeapon;

        [SerializeField] private bool _showOverWeapon;

        private Transform _transform;
        private Transform _sortingTransform;
        private Transform _visualsTransform;
        private BulletComponent _bullet;

        public void SetSortPosition(Vector2 position)
        {
            _visualsTransform.SetParent(_transform);
            _sortingTransform.position = position;
            _visualsTransform.SetParent(_sortingTransform);
        }

        private void OnHit(Collider2D other)
        {
            float y = other.transform.position.y;

            _visualsTransform.SetParent(_transform);
            _sortingTransform.position = new Vector2(_sortingTransform.position.x, y);
            _visualsTransform.SetParent(_sortingTransform);
        }

        private void Awake()
        {
            _sortingTransform = GetComponentInChildren<SortingGroup>().transform;
            _visualsTransform = _sortingTransform.GetComponentInChildren<BulletVisuals>().transform;
            _bullet = GetComponent<BulletComponent>();
            _transform = transform;
        }

        private void OnEnable()
        {
            _bullet.onHit += OnHit;
        }

        private void OnDisable()
        {
            _bullet.onHit -= OnHit;
        }
    }
}