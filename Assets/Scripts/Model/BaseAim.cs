using System;


namespace FirstShooter
{
    public abstract class BaseAim : BaseObjectScene, ICollision, ISelectedObj
    {
        #region Fields

        public float Hp = 100.0f;
        protected bool _isDead = false;
        protected float _timeToDestroy = 10.0f;

        #endregion


        #region ICollision

        public virtual void CollisionEnter(InfoCollision info)
        {
            if (_isDead) return;
        }

        #endregion


        #region ISelectObj

        public virtual string GetMessage()
        {
            return gameObject.name;
        }

        #endregion
    }
}