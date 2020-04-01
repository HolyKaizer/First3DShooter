using UnityEngine;
using System;


namespace FirstShooter
{
    public sealed class RocketMissle : Ammunition
    {
        #region Fields

        [SerializeField] private float _explosionRadius = 0.0f;
        [SerializeField] private LayerMask _damagableMask = 0;

        #endregion


        #region UnityMethods

        private void OnCollisionEnter(Collision collision)
        {
            DestroyAmmunition();
        }

        #endregion


        #region Methods

        private void Explode()
        {
            Collider[] collisionObjs = new Collider[100];

            var countOfOverlap = Physics.OverlapSphereNonAlloc(Position, _explosionRadius, collisionObjs, _damagableMask);
            for (int i = 0; i < countOfOverlap; i++)
            {
                if (collisionObjs[i].gameObject.TryGetComponent<ICollision>(out var collisionObj))
                {
                    collisionObj.CollisionEnter(new InfoCollision(_curDamage));
                }
            }
        }

        public override void DestroyAmmunition()
        {
            base.DestroyAmmunition();

            Explode();
        }

        #endregion 
    }
}