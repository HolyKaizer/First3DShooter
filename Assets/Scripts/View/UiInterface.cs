using UnityEngine;


namespace FirstShooter
{
    public class UiInterface
    {
        #region Fields

        private FlashLightUiText _flashLightUiText;
        private FlashLightUiBar _flashLightUiBar;
        private WeaponUiText _weaponUiText;
        private SelectionObjMessageUi _selectionObjMessageUi;

        #endregion


        #region Properties

        public FlashLightUiText LightUiText
        {
            get
            {
                if(!_flashLightUiText)
                {
                    _flashLightUiText = Object.FindObjectOfType<FlashLightUiText>();
                }
                return _flashLightUiText;
            }
        }

        public FlashLightUiBar LightUiBar
        {
            get
            {
                if(!_flashLightUiBar)
                {
                    _flashLightUiBar = Object.FindObjectOfType<FlashLightUiBar>();
                }
                return _flashLightUiBar;
            }
        }

        public WeaponUiText WeaponUiText
        {
            get
            {
                if (!_weaponUiText)
                {
                    _weaponUiText = Object.FindObjectOfType<WeaponUiText>();
                }
                return _weaponUiText;
            }
        }

        public SelectionObjMessageUi SelectionObjMessageUi
        {
            get
            {
                if (!_selectionObjMessageUi)
                {
                    _selectionObjMessageUi = Object.FindObjectOfType<SelectionObjMessageUi>();
                }
                return _selectionObjMessageUi;
            }
        }

        #endregion
    }
}

