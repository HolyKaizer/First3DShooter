using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;


namespace FirstShooter
{
    public sealed class Inventory: IInitialization
    {
        #region Fields

        public List<Weapon>  Weapons => _weapons;
        
        private List<Weapon> _weapons;

        private int _currentWeaponIndex;
        
        #endregion


        #region Properties 

        public FlashLightModel FlashLight { get; private set; }

        #endregion


        #region IInitialization

        public void Initialization ()
        {
            _weapons = ServiceLocatorMonoBehaviour.
                        GetService<CharacterController>().
                         GetComponentsInChildren<Weapon>().ToList();
            foreach (var weapon in Weapons)
            {
                weapon.IsVisible = false;
            }
            
            FlashLight = Object.FindObjectOfType<FlashLightModel>();
            FlashLight.Switch(FlashLightActiveType.Off);
        }

        #endregion


        #region Methods

        public Weapon GetWeapon(int weaponIndex = 0)
        {
            _currentWeaponIndex = weaponIndex;
            return Weapons[weaponIndex];
        }

        public void RemoveWeapon(Weapon weapon)
        {
            _weapons.Remove(weapon);
        }

        public void AddWeapon(Weapon weapon)
        {
            _weapons.Add(weapon);
        }

        public void AddClipsToWeapon(AmmunitionType ammoType, Clip[] clips)
        {
            for(int index = 0; index < _weapons.Count; index++)
            {
                var weapon = _weapons[index];
                if (weapon.AmmunitionTypes[0] == ammoType)
                {
                    foreach (var clip in clips)
                    {
                        weapon.AddClip(clip);
                        
                        if (_currentWeaponIndex == index)
                            UiInterface.WeaponUiText.ShowData(weapon.Clip.CountAmmunition, weapon.CountClip);
                    }
                }
            }
        }

        #endregion
    }
}