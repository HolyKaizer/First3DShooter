using UnityEngine;


namespace FirstShooter
{
    public static class UiInterface
    {
        #region Fields

        private static FlashLightUiText _flashLightUiText;
        private static FlashLightUiBar _flashLightUiBar;
        private static WeaponUiText _weaponUiText;
        private static SelectionObjMessageUi _selectionObjMessageUi;

        #endregion


        #region Properties

        public static FlashLightUiText LightUiText
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

        public static FlashLightUiBar LightUiBar
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

        public static WeaponUiText WeaponUiText
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

        public static SelectionObjMessageUi SelectionObjMessageUi
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

