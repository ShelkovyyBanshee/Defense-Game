using Pausing;
using System;
using UnityEngine;

namespace DefenseGame
{
    public class NormalLevel : Level
    {
        [Header("Main hero")]
        [SerializeField] private MainHero _mainHeroPrefab;
        [SerializeField] private Vector2 _mainHeroStartPosition;

        private CharacterController _mainHeroController;
        private MainHero _mainHero;

        private Weapon[] _weaponPrefabs;
        private RechargingSystem _rechargingSystem;

        public void SetNewWeaponPrefabs(Weapon[] weaponPrefabs)
        {
            _weaponPrefabs = weaponPrefabs;
        }

        public void InitializeRechargingSystem()
        {
            var emptyObj = new GameObject("[RECHARGING SYSTEM]");
            emptyObj.transform.SetParent(transform);

            _rechargingSystem = emptyObj.AddComponent<RechargingSystem>();
            _rechargingSystem.Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();
            InitializeRechargingSystem();
        }

        public override void StartLevel()
        {
            base.StartLevel();

            int weaponsAmount = _weaponPrefabs.Length;
            var weapons = new Weapon[weaponsAmount];

            for (int i = 0; i < weaponsAmount; i++)
            {
                weapons[i] = Fabric.Create(_weaponPrefabs[i]);

                if (weapons[i] is IChargeable)
                    _rechargingSystem.AddChargeable((IChargeable)weapons[i]);
            }

            _mainHeroController.SetWeapons(weapons);
            _mainHero.Position = _mainHeroStartPosition;
        }

        protected override void PrepareBeforeRestart()
        {
            base.PrepareBeforeRestart();

            _mainHeroController.ClearWeapons();
            _mainHero.TakeWeaponAway();
        }

        protected override IPlayerCharacter CreatePlayerCharacter()
        {
            _mainHero = Fabric.Create(_mainHeroPrefab);

            _mainHeroController = _mainHero.GetComponent<CharacterController>();

            return _mainHero;
        }
    }
}