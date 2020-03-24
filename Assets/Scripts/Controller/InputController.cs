using UnityEngine;


namespace Geekbrains
{
    public sealed class InputController : BaseController, IExecute
    {
        #region Fields

        private KeyCode _activeFlashLight = KeyCode.F;
        private KeyCode _freezePositionZ = KeyCode.L;

        #endregion


        #region IExecute

        public void Execute()
        {
            if (!IsActive) return;

            if (Input.GetKeyDown(_activeFlashLight))
            {
                ServiceLocator.Resolve<FlashLightController>().Switch();
            }
        }

        #endregion
    }
}
