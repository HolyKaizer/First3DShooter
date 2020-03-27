using System;
using UnityEngine;


namespace FirstShooter
{
    public class Aim : BaseObjectScene, ICollision, ISelectedObj
    {
        #region Fields

        public event Action OnPointChange = delegate { };

        public float Hp = 100;
        private bool _isDead;
        private float _timeToDestroy = 10.0f;

        #endregion


        #region ICollision

        //todo дописать поглащение урона
        public void CollisionEnter(InfoCollision info)
        {
            if (_isDead) return;
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

                OnPointChange.Invoke();
                _isDead = true;
            }
        }

        #endregion


        #region ISelectObj

        public string GetMessage()
        {
            return gameObject.name;
        }

        #endregion
    }
}