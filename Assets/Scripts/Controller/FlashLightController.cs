using UnityEngine;


namespace FirstShooter
{
    public sealed class FlashLightController : BaseController, IExecute, IInitialization
    {
        #region Fields

        private FlashLightModel _flashLightModel;
        
        #endregion


        #region IInitialization

        public void Initialization()
        {
            _flashLightModel = Object.FindObjectOfType<FlashLightModel>();
        }

        #endregion


        #region Methods

        public override void On()
        {
            if(IsActive) return;
            if (_flashLightModel.BatteryChargeCurrent <= 0) return;
            base.On();

            _flashLightModel.Switch(FlashLightActiveType.On);
            UiInterface.LightUiBar.SetActive(true);
            UiInterface.LightUiText.SetActive(true);
        }

        public override void Off()
        {
            if (!IsActive) return;
            base.Off();

            _flashLightModel.Switch(FlashLightActiveType.Off);
            UiInterface.LightUiBar.SetActive(false);
            UiInterface.LightUiText.SetActive(false);
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
                UiInterface.LightUiBar.Slider = _flashLightModel.BatteryChargeCurrent;
                UiInterface.LightUiText.Text = _flashLightModel.BatteryChargeCurrent;
            }
            else
            {
                Off();
            }
        }

        #endregion
    }
}
