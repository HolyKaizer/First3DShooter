using UnityEngine;


namespace FirstShooter
{
    public abstract class Ammunition : BaseObjectScene
    {
        #region Fields

        [SerializeField] private float _timeToDestruct = 5.0f;
        [SerializeField] private float _baseDamage = 10.0f;

        protected float _curDamage;

        private ITimeRemaining _destroyTimeRemaining;
        private Vector3 _curForce;

        public AmmunitionType Type = AmmunitionType.Bullet;

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            _destroyTimeRemaining = new TimeRemaining(DestroyAmmunition, _timeToDestruct);
        }

        protected virtual void OnEnable()
        {
            _curDamage = _baseDamage;

            _destroyTimeRemaining.AddTimeRemaining();
        }

        #endregion


        #region Methods

        public void AddForce(Vector3 force)
        {
            if (!Rigidbody) return;
            _curForce = force;
            Rigidbody.AddForce(_curForce);
        }

        public virtual void DestroyAmmunition()
        {
            _destroyTimeRemaining.RemoveTimeRemaining();
            
            if (Rigidbody) Rigidbody.velocity = Vector3.zero;

            Rotation = Quaternion.identity;
            SetActive(false);
        }

        #endregion
    }
}