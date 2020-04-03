using UnityEngine;


namespace FirstShooter
{
    public sealed class CatchController : BaseController
    {
        #region Fields

        private readonly LayerMask _catchableLayerMask;

        private readonly Camera _mainCamera;
        private readonly Vector2 _center;
        private readonly float _dedicateCatchDistance = 4.0f;

        private ICatchaleObj _catchedObj;
        private bool _isCurrentlyCarryObj = false;

        #endregion


        #region ClassLifeCycle

        public CatchController() : base()
        {
            _mainCamera = Camera.main;
            _center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
            _catchableLayerMask = ServiceLocatorMonoBehaviour.GetService<GameController>().GameplayData.SelecatbleLayerMask;
        }

        #endregion


        #region Methods

        public void CatchObject()
        {
            if (_isCurrentlyCarryObj)
            {
                ThrowObject();
                return;
            }
            if (!Physics.Raycast(_mainCamera.ScreenPointToRay(_center),
                                out var hit,
                                _dedicateCatchDistance,
                                _catchableLayerMask))
                return;

            _catchedObj = hit.collider.gameObject.GetComponent<ICatchaleObj>();

            if (_catchedObj != null)
            {
                _isCurrentlyCarryObj = true;
                _catchedObj.CatchObject();
            }
        }

        private void ThrowObject()
        {
            _isCurrentlyCarryObj = false;
            _catchedObj.ThrowObject();
            _catchedObj = null;
        }

        #endregion
    }
}