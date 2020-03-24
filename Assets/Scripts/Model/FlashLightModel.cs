using System;
using UnityEngine;


namespace Geekbrains
{
    public sealed class FlashLightModel : BaseObjectScene
    {
        #region Fields

        [SerializeField] private float _speed = 11;
        [SerializeField] private float _batteryChargeMax;

        private Light _light;
        private Transform _goFollow;
        private Vector3 _vecOffset;

        #endregion


        #region Properties

        public float BatteryChargeCurrent { get; private set; }

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();

            _light = GetComponent<Light>();

            _goFollow = Camera.main.transform;
            _vecOffset = Position - _goFollow.position;

            BatteryChargeCurrent = _batteryChargeMax;
        }

        #endregion


        #region Methods

        public void Switch(FlashLightActiveType value)
        {
            switch (value)
            {
                case FlashLightActiveType.On:
                    _light.enabled = true;
                    Position = _goFollow.position + _vecOffset;
                    Rotation = _goFollow.rotation;
                    break;
                case FlashLightActiveType.Off:
                    _light.enabled = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public void RotateObject()
        {
            Position = _goFollow.position + _vecOffset;

            Rotation = Quaternion.Lerp(Rotation,
                                        _goFollow.rotation,
                                        _speed * Time.deltaTime);
        }

        public bool EditBatteryCharge()
        {
            if (BatteryChargeCurrent > 0)
            {
                BatteryChargeCurrent -= Time.deltaTime;
                return true;
            }
            else
            {
                BatteryChargeCurrent = _batteryChargeMax;
            }

            return false;
        }

        #endregion
    }
}
