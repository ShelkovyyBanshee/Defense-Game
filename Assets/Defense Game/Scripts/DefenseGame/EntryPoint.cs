using UnityEngine;
using Pooling;
using Pausing;
using System.Collections.Generic;


namespace DefenseGame
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] SingleEnemySpawner[] _spawners;
        [SerializeField] Weapon[] _weaponPrefabs;

        private NormalLevel _level;

        private void Start()
        {
            _level = GameObject.FindGameObjectWithTag("Level").GetComponent<NormalLevel>();

            _level.Initialize();
            _level.SetNewWeaponPrefabs(_weaponPrefabs);

            foreach (var spawner in _spawners)
                spawner.Initialize(_level.PoolingSystem);

            _level.StartLevel();

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _level.CloseLevel();
                _level.SetNewWeaponPrefabs(_weaponPrefabs);
                _level.RestartLevel();
            }
        }
    }
}