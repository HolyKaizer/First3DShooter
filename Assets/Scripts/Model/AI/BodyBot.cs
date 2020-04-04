using System;


namespace FirstShooter
{
    public sealed class BodyBot : BaseObjectScene, ICollision
    {
        #region Fields

        public event Action<InfoCollision> OnApplyDamageChange;

        #endregion


        #region ICollision

        public void CollisionEnter(InfoCollision collisionInfo)
        {
            OnApplyDamageChange?.Invoke(collisionInfo);
        }

        #endregion
    }
}