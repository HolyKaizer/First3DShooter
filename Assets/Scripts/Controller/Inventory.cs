using UnityEngine;


namespace FirstShooter
{
    public sealed class Inventory: IInitialization
    {
        #region Fields

        public Weapon[] Weapons => _weapons;
        
        private Weapon[] _weapons = new Weapon[5];

        private int _currentWeaponIndex = 0;
        private int _hp;
        
        #endregion


        #region Properties 

        public FlashLightModel FlashLight { get; private set; }

        public int Hp
        {
            get => _hp;

            set
            {
                _hp = value;
                UiInterface.PlayerCurrentHpText.Text = _hp.ToString();
            }
        }

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
            _currentWeaponIndex = weaponIndex;
            return Weapons[weaponIndex];
        }

        public void RemoveWeapon(Weapon weapon)
        {

        }

        public void AddClipsToWeapon(AmmunitionType ammoType, Clip[] clips)
        {
            for(int index = 0; index < _weapons.Length; index++)
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