using UnityEngine;
using System.Collections.Generic;


namespace FirstShooter
{
    public abstract class Weapon : BaseObjectScene
    {
		#region Fields

		public Ammunition Ammunition;
		[HideInInspector] public Clip Clip;

		[HideInInspector] public AmmunitionType[] AmmunitionTypes = { AmmunitionType.Bullet };

		[SerializeField] private int _maxCountAmmunition = 40;
		[SerializeField] private int _minCountAmmunition = 20;
		[SerializeField] private int _countClip = 5;

		[SerializeField] protected Transform _barrel;
		[SerializeField] protected float _force = 1000.0f;
		[SerializeField] protected float _rechargeTime = 0.2f;

		private readonly Queue<Clip> _clips = new Queue<Clip>();
		protected ObjectPool _ammunitionPool;

		protected bool _isReady = true;
		protected ITimeRemaining _attackRateTimeRemaining;

        #endregion


        #region Properties

        public int CountClip => _clips.Count;

        #endregion


        #region UnityMethods

        private void Start()
		{
			_ammunitionPool = new ObjectPool(Ammunition, _maxCountAmmunition, true);

			_attackRateTimeRemaining = new TimeRemaining(ReadyShoot, _rechargeTime);
            
			for (var i = 0; i <= _countClip; i++)
			{
				AddClip(new Clip { CountAmmunition =  _maxCountAmmunition });
			}

			ReloadClip();
		}

        #endregion


        #region Methods

        public abstract void Fire();

		protected void ReadyShoot()
		{
			_isReady = true;
		}

		protected void AddClip(Clip clip)
		{
			_clips.Enqueue(clip);
		}

		public void ReloadClip()
		{
			if (CountClip <= 0) return;

			Clip = _clips.Dequeue();
		}

        #endregion
    }
}

