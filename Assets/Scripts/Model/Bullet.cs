using UnityEngine;


namespace FirstShooter
{
    public sealed class Bullet : Ammunition
    {
        #region Fields

        [SerializeField] private float _lossOfDamageAtTime = 0.2f;

        private ITimeRemaining _lossDamageTimeRemaining;

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            _lossDamageTimeRemaining = new TimeRemaining(LossOfDamage, 0.5f, true);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _lossDamageTimeRemaining.AddTimeRemaining();
        }

        private void OnCollisionEnter(Collision collision) 
        {
            var collisionObj = collision.gameObject.GetComponent<ICollision>();

            if (collisionObj != null)
            {
                collisionObj.CollisionEnter(new InfoCollision(_curDamage, collision.contacts[0], transform, Rigidbody.velocity));
                
            }

            DestroyAmmunition();
        }

        #endregion


        #region Methods

        private void LossOfDamage()
        {
            _curDamage -= _lossOfDamageAtTime;
        }

        public override void DestroyAmmunition()
        {
            base.DestroyAmmunition();

            _lossDamageTimeRemaining.RemoveTimeRemaining();
        }

        #endregion
    }
}