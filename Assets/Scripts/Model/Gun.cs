﻿namespace FirstShooter
{
    public sealed class Gun : Weapon
    {
        #region Methods

        public override void Fire()
        {
            if (!_isReady) return;
            if (Clip.CountAmmunition <= 0) return;

            var temAmmunition = _ammunitionPool.GetAvailableObject() as Ammunition;
            
            temAmmunition.Position = _barrel.position;
            temAmmunition.Rotation = _barrel.rotation;
            temAmmunition.SetActive(true);
            temAmmunition.AddForce(_barrel.forward * _force);

            Clip.CountAmmunition--;
            _isReady = false;
            _attackRateTimeRemaining.AddTimeRemaining();
        }

        #endregion
    }
}