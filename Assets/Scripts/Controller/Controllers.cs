using UnityEngine;


namespace FirstShooter
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
;
            ServiceLocator.SetService(new TimeRemainingController());
            ServiceLocator.SetService(new Inventory());
            ServiceLocator.SetService(new PlayerController(motor));
            ServiceLocator.SetService(new FlashLightController());
            ServiceLocator.SetService(new WeaponController());
            ServiceLocator.SetService(new InputController());
            ServiceLocator.SetService(new SelectionController());
            ServiceLocator.SetService(new CatchController());
            ServiceLocator.SetService(new BotController());

            _executeControllers = new IExecute[6];

            _executeControllers[0] = ServiceLocator.Resolve<TimeRemainingController>();

            _executeControllers[1] = ServiceLocator.Resolve<InputController>(); 

            _executeControllers[2] = ServiceLocator.Resolve<PlayerController>(); 

            _executeControllers[3] = ServiceLocator.Resolve<FlashLightController>();

            _executeControllers[4] = ServiceLocator.Resolve<SelectionController>();

            _executeControllers[5] = ServiceLocator.Resolve<BotController>();
        }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            UiInterface.LightUiBar.SetActive(false);

            foreach (var controller in _executeControllers)
            {
                if (controller is IInitialization initialization)
                {
                    initialization.Initialization();
                }
            }

            ServiceLocator.Resolve<Inventory>().Initialization();
            ServiceLocator.Resolve<InputController>().On();
            ServiceLocator.Resolve<CatchController>().On();
            ServiceLocator.Resolve<SelectionController>().On();
            ServiceLocator.Resolve<PlayerController>().On();
            ServiceLocator.Resolve<BotController>().On();
        }

        #endregion
    }
}
