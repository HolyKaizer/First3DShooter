using UnityEngine;
using System.Collections.Generic;


namespace FirstShooter
{
    public class ObjectPool
    {
        #region Fields

        private int _poolDepth;
        private bool _canGrow;

        private readonly List<BaseObjectScene> _pool = new List<BaseObjectScene>();

        #endregion


        #region Properties

        public BaseObjectScene ObjectPrefab { get; private set; }

        #endregion


        #region ClassLifeCycle

        public ObjectPool(BaseObjectScene prefab, int depth, bool canGrow = false)
        {
            ObjectPrefab = prefab;
            _poolDepth = depth; 
            _canGrow = canGrow;

            for (int i = 0; i < _poolDepth; i++)
            {
                BaseObjectScene pooledObject = Object.Instantiate(ObjectPrefab, Vector3.zero, Quaternion.identity);
                pooledObject.SetActive(false);
                _pool.Add(pooledObject);
            }
        }

        #endregion


        #region Methods

        public BaseObjectScene GetAvailableObject()
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                if (!_pool[i].ActiveInHierarchy)
                {
                    return _pool[i];
                }
            }

            if (_canGrow)
            {
                BaseObjectScene pooledObject = Object.Instantiate(ObjectPrefab, Vector3.zero, Quaternion.identity);
                pooledObject.SetActive(false);
                _pool.Add(pooledObject);
                return pooledObject;
            }

            return null;
        }

        #endregion
    }
}