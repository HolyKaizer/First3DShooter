using UnityEngine;
using UnityEngine.UI;


namespace FirstShooter
{
    [RequireComponent(typeof(Slider))]
    public sealed class FlashLightUiBar : MonoBehaviour
    {
        #region Fields

        private Slider _slider;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.value = _slider.maxValue;
        }

        #endregion


        #region Properties

        public float Slider
        {
            set => _slider.value = value;
        }

        #endregion


        #region Methods

        public void SetActive(bool value)
        {
            _slider.gameObject.SetActive(value);
        }

        #endregion
    }
}