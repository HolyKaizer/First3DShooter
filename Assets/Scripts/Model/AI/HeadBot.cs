using System;
using UnityEngine;



namespace FirstShooter
{
    public sealed class HeadBot: BaseObjectScene, ICollision
    {
        #region Fields

        [SerializeField] private float _headshotDamageMultuplier = 10;
        public event Action<InfoCollision> OnApplyDamageChange;
        public event Action<InfoCollision> OnHealingChange;

        #endregion


        #region ICollision

        public void CollisionEnter(InfoCollision collisionInfo)
        {
            if (collisionInfo.CollisionType == CollisionType.DamageDealt)
            {
                OnApplyDamageChange?.Invoke(new InfoCollision(collisionInfo.Damage * _headshotDamageMultuplier,
                    collisionInfo.Contact,
                    collisionInfo.ObjCollision));
            }
            else if (collisionInfo.CollisionType == CollisionType.Healing)
            {
                OnHealingChange?.Invoke(collisionInfo);
            }
        }
        

        #endregion
    }
}