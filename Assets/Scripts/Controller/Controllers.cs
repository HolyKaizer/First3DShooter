using UnityEngine;


namespace Geekbrains
{
    public sealed class Controllers : IInitialization
    {
        #region Fields

        private readonly IExecute[] _executeControllers;

        public int Length => _executeControllers.Length;

        public IExecute this[int index] => _executeControllers[index];

        #endregion


        #region ClassLifeCycle

        public Controllers()
        {
            IMotor motor = default;
            if (Application.platform == RuntimePlatform.PS4)
            {
                //
            }
            else
            {
                motor = new UnitMotor(
                    ServiceLocatorMonoBehaviour.GetService<CharacterController>());
            }

            ServiceLocator.SetService(new PlayerController(motor));
            ServiceLocator.SetService(new FlashLightController());
            ServiceLocator.SetService(new InputController());
            _executeControllers = new IExecute[3];

            _executeControllers[0] = ServiceLocator.Resolve<PlayerController>();

            _executeControllers[1] = ServiceLocator.Resolve<FlashLightController>();

            _executeControllers[2] = ServiceLocator.Resolve<InputController>();
        }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            foreach (var controller in _executeControllers)
            {
                if (controller is IInitialization initialization)
                {
                    initialization.Initialization();
                }
            }
            
            ServiceLocator.Resolve<InputController>().On();
            ServiceLocator.Resolve<PlayerController>().On();
        }

        #endregion
    }
}
