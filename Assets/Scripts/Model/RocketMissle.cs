using UnityEngine;


namespace FirstShooter
{
    public sealed class RocketMissle : Ammunition
    {
        #region Fields

        [SerializeField] private float _explosionRadius;
        [SerializeField] private LayerMask _damagableMask;

        #endregion


        #region UnityMethods

        private void OnCollisionEnter(Collision collision)
        {
            Explode();

            DestroyAmmunition();
        }

        #endregion


        #region Methods

        private void Explode()
        {
            var collisionObjs = Physics.OverlapSphere(Position, _explosionRadius);

            for (int i = 0; i < collisionObjs.Length; i++)
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