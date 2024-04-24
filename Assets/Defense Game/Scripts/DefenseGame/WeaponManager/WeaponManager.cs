using System;
using System.Collections.Generic;
using UnityEngine;


namespace DefenseGame
{
    public class WeaponManager
    {
        public Weapon CurrentWeapon => _weapons == null ? null : _weapons[_currentWeaponIndex];
        
        public int CurrentWeaponIndex => _currentWeaponIndex;
        public int WeaponsAmount => _weapons.Count;

        private List<Weapon> _weapons;
        private int _currentWeaponIndex;

        private IControllable _weaponUser;
        private Transform _weaponsDirectory;
        
        public WeaponManager(IControllable weaponUser, Transform weaponsDirectory)
        {
            _weaponUser = weaponUser;
            _weaponsDirectory = weaponsDirectory;
        }

        public void DestroyOldWeapons()
        {
            foreach (var weapon in _weapons)
            {
                MonoBehaviour.Destroy(weapon.gameObject);
            }
        }

        public void UpdateWeapons(Weapon[] weapons)
        {
            _weapons = new List<Weapon>();

            foreach (var weapon in weapons)
            {
                weapon.gameObject.SetActive(false);
                
                weapon.transform.SetParent(_weaponsDirectory);
                _weapons.Add(weapon);
            }

            _currentWeaponIndex = 0;
            UpdateWeapon(false);
        }

        public void SwitchToNextWeapon(bool playAnimation)
        {
            _currentWeaponIndex = _currentWeaponIndex + 1 < _weapons.Count
                ? _currentWeaponIndex + 1 : 0;

            UpdateWeapon(playAnimation);
        }

        public void SwitchToPreviousWeapon(bool playAnimation)
        {
            _currentWeaponIndex = _currentWeaponIndex - 1 >= 0
                ? _currentWeaponIndex - 1 : _weapons.Count - 1;

            UpdateWeapon(playAnimation);
        }

        public void SwitchByMove(int move, bool playAnimation)
        {
            int newIndex;

            if (move == 0)
                return;
            else if (_currentWeaponIndex + move >= 0)
                newIndex = (_currentWeaponIndex + move) % _weapons.Count;
            else
            {
                int k = Math.Abs(_currentWeaponIndex + move) % (_weapons.Count);
                newIndex = k == 0 ? 0 : _weapons.Count - k;
            }
           

            if (newIndex != _currentWeaponIndex)
            {
                _currentWeaponIndex = newIndex;
                UpdateWeapon(playAnimation);
            }
        }

        public void SwitchToIndex(int index, bool playAnimation)
        {
            if (index == _currentWeaponIndex)
                return;

            _currentWeaponIndex = index;
            UpdateWeapon(playAnimation);
        }

        private void UpdateWeapon(bool playAnimation)
        {
            var newWeapon = _weapons[_currentWeaponIndex];
            newWeapon.gameObject.SetActive(true);

            newWeapon.OnEquip(playAnimation);

            var previousWeapon = _weaponUser.SwitchWeapon(newWeapon);
  
            if (previousWeapon != null)
            {
                previousWeapon.transform.SetParent(_weaponsDirectory);
                previousWeapon.gameObject.SetActive(false);
            }
        }
    }
}