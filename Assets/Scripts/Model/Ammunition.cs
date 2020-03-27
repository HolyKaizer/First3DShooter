using UnityEngine;


namespace FirstShooter
{
    public abstract class Ammunition : BaseObjectScene
    {
        #region Fields

        [SerializeField] private float _timeToDestruct = 10;
        [SerializeField] private float _baseDamage = 10;
 
        protected float _curDamage; // todo доделать свой урон
        private float _lossOfDamageAtTime = 0.2f;
        private ITimeRemaining _timeRemaining;

        public AmmunitionType Type = AmmunitionType.Bullet;

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            _curDamage = _baseDamage;
        }

        private void Start()
        {
            Destroy(gameObject, _timeToDestruct);
            _timeRemaining = new TimeRemaining(LossOfDamage, 1.0f, true);
            _timeRemaining.AddTimeRemaining();
        }

        #endregion


        #region Methods

        public void AddForce(Vector3 dir)
        {
            if (!Rigidbody) return;
            Rigidbody.AddForce(dir);
        }

        private void LossOfDamage()
        {
            _curDamage -= _lossOfDamageAtTime;
        }

        protected void DestroyAmmunition()
        {
            Destroy(gameObject);
            _timeRemaining.RemoveTimeRemaining();
            // Вернуть в пул
        }

        #endregion
    }
}