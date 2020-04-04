using System;
using UnityEngine;



namespace FirstShooter
{
    public sealed class HeadBot: BaseObjectScene, ICollision
    {
        #region Fields

        [SerializeField] private float _headshotDamageMultuplier = 10;
        public event Action<InfoCollision> OnApplyDamageChange;

        #endregion


        #region ICollision

        public void CollisionEnter(InfoCollision collisionInfo)
        {
            OnApplyDamageChange?.Invoke(new InfoCollision(collisionInfo.Damage * _headshotDamageMultuplier,
                                                            collisionInfo.Contact,
                                                            collisionInfo.ObjCollision));
        }

        #endregion
    }
}