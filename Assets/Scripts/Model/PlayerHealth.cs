using System;
using System.Collections;
using UnityEngine;


namespace FirstShooter
{
    public sealed class PlayerHealth : BaseObjectScene, ICollision
    {
        #region Fields

        [SerializeField] private float _hp;
        [SerializeField] private float _maxHp;

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            
            
            UiInterface.PlayerCurrentHpText.SetActive(true);
            UiInterface.PlayerCurrentHpText.Text = ((int)_hp).ToString();
        }

        #endregion
        
        
        #region ICollision

        public void CollisionEnter(InfoCollision infoCollision)
        {
            if (infoCollision.CollisionType == CollisionType.DamageDealt)
            {
                GetDamage(infoCollision.Damage);
            }
            else if (infoCollision.CollisionType == CollisionType.Healing)
            {
                Heal(infoCollision.Damage);
            }
        }

        #endregion


        #region Methods

        private void GetDamage(float damage)
        {
            if (_hp > 0)
            {
                _hp -= damage;
            }

            if (_hp <= 0)
            {
                Die();
            }

            ServiceLocator.Resolve<Inventory>().Hp = (int)_hp;
        }
        
        private void Heal(float amount)
        {
            if (_hp + amount > _maxHp)
            {
                _hp = _maxHp;
            }
            else
            {
                _hp += amount;
            }

            ServiceLocator.Resolve<Inventory>().Hp = (int)_hp;
        }

        private void Die()
        {
            Debug.LogError("Character DIED");
        }

        #endregion
    }
}