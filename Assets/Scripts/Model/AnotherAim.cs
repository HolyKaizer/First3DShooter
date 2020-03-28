using System;
using UnityEngine;


namespace FirstShooter
{
    public sealed class AnotherAim : BaseAim
    {
        #region Fields

        public event Action OnDestroyChange = delegate { };

        #endregion


        #region ICollision

        public override void CollisionEnter(InfoCollision info)
        {
            base.CollisionEnter(info);

            if (Hp > 0)
            {
                Hp -= info.Damage;
            }

            if (Hp <= 0)
            {
                if (!TryGetComponent<Rigidbody>(out _))
                {
                    gameObject.AddComponent<Rigidbody>();
                }
                Destroy(gameObject, _timeToDestroy);

                OnDestroyChange.Invoke();
                _isDead = true;
            }
        }

        #endregion
    }
}