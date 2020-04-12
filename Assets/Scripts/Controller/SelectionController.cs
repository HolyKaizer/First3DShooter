using UnityEngine;


namespace FirstShooter
{
    public sealed class SelectionController : BaseController, IExecute
    {
        #region Fields

        private readonly Camera _mainCamera;
        private readonly Vector2 _center;
        private readonly float _dedicateDistance = 20.0f;

        private GameObject _dedicateObj;
        private ISelectedObj _selectedObj;
        private readonly LayerMask _selectableLayerMask;

        private bool _nullString = false;

        #endregion


        #region ClassLifeCycle

        public SelectionController() : base()
        { 
            _mainCamera = Camera.main;
            _center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
            _selectableLayerMask = ServiceLocatorMonoBehaviour.GetService<GameController>().GameplayData.SelecatbleLayerMask;
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (!IsActive) return;

            if(Physics.Raycast(_mainCamera.ScreenPointToRay(_center),
                                out var hit,
                                _dedicateDistance,
                                _selectableLayerMask))
            {
                SelectObject(hit.collider.gameObject);
                _nullString = false;
            }
            else if(!_nullString)
            {
                UiInterface.SelectionObjMessageUi.Text = string.Empty;
                _nullString = true;
                _dedicateObj = null;
            }
        }

        #endregion


        #region Methods

        private void SelectObject(GameObject obj)
        {
            if (obj == _dedicateObj) return;

            _selectedObj = obj.GetComponent<ISelectedObj>();
            if(_selectedObj != null)
            {
                UiInterface.SelectionObjMessageUi.Text = _selectedObj.GetMessage();
            }
            else
            {
                UiInterface.SelectionObjMessageUi.Text = string.Empty;
            }
            _dedicateObj = obj;
        }

        #endregion
    }
}

