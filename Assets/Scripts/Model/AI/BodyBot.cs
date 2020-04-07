using System;


namespace FirstShooter
{
    public sealed class BodyBot : BaseObjectScene, ICollision
    {
        #region Fields

        public event Action<InfoCollision> OnApplyDamageChange;
        public event Action<InfoCollision> OnHealingChange;

        #endregion


        #region ICollision

        public void CollisionEnter(InfoCollision collisionInfo)
        {
            if (collisionInfo.CollisionType == CollisionType.DamageDealt)
            {
                OnApplyDamageChange?.Invoke(collisionInfo);
            }
            else if (collisionInfo.CollisionType == CollisionType.Healing)
            {
                OnHealingChange?.Invoke(collisionInfo);
            }
        }

        #endregion
    }
}