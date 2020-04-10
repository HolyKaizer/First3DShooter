using System;
using System.Collections;
using UnityEngine;


namespace FirstShooter
{
    public sealed class PlayerHealth : BaseObjectScene, ICollision
    {
        #region Fields

        public float Hp;
        public float MaxHp;

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            
            
            UiInterface.PlayerCurrentHpText.SetActive(true);
            UiInterface.PlayerCurrentHpText.Text = ((int)Hp).ToString();
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
            if (Hp > 0)
            {
                Hp -= damage;
            }

            if (Hp <= 0)
            {
                Die();
            }

            ServiceLocator.Resolve<Inventory>().Hp = (int)Hp;
        }
        
        private void Heal(float amount)
        {
            if (Hp + amount > MaxHp)
            {
                Hp = MaxHp;
            }
            else
            {
                Hp += amount;
            }

            ServiceLocator.Resolve<Inventory>().Hp = (int)Hp;
        }

        private void Die()
        {
            Debug.LogError("Character DIED");
        }

        #endregion
    }
}