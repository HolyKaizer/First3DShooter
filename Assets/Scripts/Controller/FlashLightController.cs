using UnityEngine;


namespace Geekbrains
{
    public sealed class FlashLightController : BaseController, IExecute, IInitialization
    {
        #region Fields

        private FlashLightModel _flashLightModel;
        private FlashLightUi _flashLightUi;
        
        #endregion


        #region IInitialization

        public void Initialization()
        {
            _flashLightModel = Object.FindObjectOfType<FlashLightModel>();
            _flashLightUi = Object.FindObjectOfType<FlashLightUi>();
        }

        #endregion


        #region Methods

        public override void On()
        {
            if(IsActive) return;
            if (_flashLightModel.BatteryChargeCurrent <= 0) return;
            base.On();

            _flashLightModel.Switch(FlashLightActiveType.On);
            _flashLightUi.SetActive(true);
        }

        public override void Off()
        {
            if (!IsActive) return;
            base.Off();

            _flashLightModel.Switch(FlashLightActiveType.Off);
            _flashLightUi.SetActive(false);
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if(!IsActive)
            {
                return;
            }

            _flashLightModel.RotateObject();
            if (_flashLightModel.EditBatteryCharge())
            {
                _flashLightUi.Slider = _flashLightModel.BatteryChargeCurrent;
            }
            else
            {
                Off();
            }
        }

        #endregion
    }
}
