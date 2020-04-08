using System;
using System.Security.Cryptography;
using UnityEngine;


namespace FirstShooter
{
    public sealed class Mine : BaseTriggerableObject
    {
        #region Fields

        [SerializeField] private int _damage;

        #endregion
        
        
        #region UnityMethods

        private void OnTriggerEnter(Collider collider)
        {
            Trigger(collider, new TriggerInfo(_damage, CollisionType.DamageDealt));
        }

        #endregion
    }
}