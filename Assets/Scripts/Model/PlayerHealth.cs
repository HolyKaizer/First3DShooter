using System;
using System.Collections;
using UnityEngine;


namespace FirstShooter
{
    public sealed class PlayerHealth : BaseObjectScene, ICollision
    {
        #region Fields

        public Action<int> OnHealthChange = delegate(int i) {  };
        public float Hp;
        public float MaxHp;

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
            
            OnHealthChange.Invoke((int)Hp);
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

            OnHealthChange.Invoke((int)Hp);
        }

        private void Die()
        {
            Debug.LogError("Character DIED");
        }

        #endregion
    }
}