using UnityEngine;


namespace FirstShooter
{
    public sealed class InputController : BaseController, IExecute
    {
        #region Fields

        private KeyCode _activeFlashLight = KeyCode.F;
        private KeyCode _cancel = KeyCode.Escape;
        private KeyCode _reloadClip = KeyCode.R;
        private int _mouseButton = (int)MouseButton.LeftButton;

        #endregion


        #region ClassLifeCycle

        public InputController()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (!IsActive) return;
            if (Input.GetKeyDown(_activeFlashLight))
            {
                ServiceLocator.Resolve<FlashLightController>().Switch(ServiceLocator.Resolve<Inventory>().FlashLight);
            }
            //todo реализовать выбор оружия по колесику мыши

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SelectWeapon(0);
            }

            if (Input.GetMouseButton(_mouseButton))
            {
                if (ServiceLocator.Resolve<WeaponController>().IsActive)
                {
                    ServiceLocator.Resolve<WeaponController>().Fire();
                }
            }

            if (Input.GetKeyDown(_cancel))
            {
                ServiceLocator.Resolve<WeaponController>().Off();
                ServiceLocator.Resolve<FlashLightController>().Off();
            }

            if (Input.GetKeyDown(_reloadClip))
            {
                if (ServiceLocator.Resolve<WeaponController>().IsActive)
                {
                    ServiceLocator.Resolve<WeaponController>().ReloadClip();
                }
            }
        }

        #endregion


        #region Methods

        private void SelectWeapon(int i)
        {
            ServiceLocator.Resolve<WeaponController>().Off();
            var tempWeapon = ServiceLocator.Resolve<Inventory>().Weapons[i]; //todo инкапсулировать
            if (tempWeapon != null)
            {
                ServiceLocator.Resolve<WeaponController>().On(tempWeapon);
            }
        }

        #endregion
    }
}
