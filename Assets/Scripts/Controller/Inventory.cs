using UnityEngine;


namespace FirstShooter
{
    public sealed class Inventory: IInitialization
    {
        #region Fields

        private Weapon[] _weapons = new Weapon[5];

        public Weapon[] Weapons => _weapons;

        #endregion


        #region Properties 

        public FlashLightModel FlashLight { get; private set; }

        #endregion


        #region IInitialization

        public void Initialization ()
        {
            _weapons = ServiceLocatorMonoBehaviour.
                        GetService<CharacterController>().
                         GetComponentsInChildren<Weapon>();
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
            return Weapons[weaponIndex];
        }

        public void RemoveWeapon(Weapon weapon)
        {

        }

        #endregion
    }
}