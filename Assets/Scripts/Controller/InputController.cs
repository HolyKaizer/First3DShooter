using UnityEngine;


namespace FirstShooter
{
    public sealed class InputController : BaseController, IExecute
    {
        #region Fields

        private KeyCode _activeFlashLight = KeyCode.F;
        private KeyCode _cancel = KeyCode.Escape;
        private KeyCode _reloadClip = KeyCode.R;
        private KeyCode _catchObject = KeyCode.E;
        private int _mouseButton = (int)MouseButton.LeftButton;
        private float _mouseScrollScale = 0.1f;

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

            if (Input.GetKeyDown(_catchObject))
            {
                ServiceLocator.Resolve<CatchController>().CatchObject();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) || (Mathf.Abs(Input.mouseScrollDelta.y * _mouseScrollScale) >= 0.1f))
            {
                SelectWeapon(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) || (Mathf.Abs(Input.mouseScrollDelta.y * _mouseScrollScale) >= 0.15f))
            {
                SelectWeapon(1);
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
            var tempWeapon = ServiceLocator.Resolve<Inventory>().GetWeapon(i); 
            if (tempWeapon != null)
            {
                ServiceLocator.Resolve<WeaponController>().On(tempWeapon);
            }
        }

        #endregion
    }
}
