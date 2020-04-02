using UnityEngine;
using System;


namespace FirstShooter
{
    public sealed class MoveableBox : BaseObjectScene, ICatchaleObj, ISelectedObj
    {
        #region Fields

        [SerializeField] private Transform _playerCatchTransform;

        private Transform _oldParent;
        private int _selectableLayer;
        private int _nothingLayer;

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();

            _oldParent = Transform.parent;

            //Calculate layer depend on layerMask
            var selectableLayerMask = ServiceLocatorMonoBehaviour.GetService<GameController>().GameplayData.SelecatbleLayerMask.value;
            _selectableLayer = (int)(Math.Log10(selectableLayerMask) / Math.Log10(2));

            var nothingLayerMask = ServiceLocatorMonoBehaviour.GetService<GameController>().GameplayData.NothingLayerMask.value;
            _nothingLayer = (int)(Math.Log10(nothingLayerMask) / Math.Log10(2));
        }

        #endregion


        #region ICatchaleObj

        public void CatchObject()
        {
            if (Rigidbody)
            {
                Rigidbody.isKinematic = true;
            }

            Layer = _nothingLayer;
            Transform.parent = _playerCatchTransform;
            Position = Transform.parent.position;
        }

        public void ThrowObject()
        {
            if (Rigidbody)
            {
                Rigidbody.isKinematic = false;
            }

            Layer = _selectableLayer;
            Transform.parent = _oldParent;
        }

        #endregion


        #region ISelectedObj

        public string GetMessage()
        {
            return gameObject.name;
        }

        #endregion
    }
}