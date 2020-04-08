using System;
using UnityEngine;


namespace FirstShooter
{
    public sealed class MedKit : BaseTriggerableObject
    {
        #region Fields

        [SerializeField] private float _healAmount = 0.0f;

        #endregion


        #region UnityMethods

        private void OnTriggerEnter(Collider collider)
        {
            Trigger(collider, new TriggerInfo(_healAmount, CollisionType.Healing));
        }

        #endregion
    }
}