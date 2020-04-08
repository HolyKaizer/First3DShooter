using UnityEngine;

namespace FirstShooter
{
    public readonly struct InfoCollision
    {
        #region Fields

        private readonly Vector3 _dir;
        private readonly float _damage;
        private readonly ContactPoint _contact;
        private readonly Transform _objCollision;
        private readonly CollisionType _collisionType;

        #endregion


        #region Properties

        public Vector3 Dir => _dir;

        public float Damage => _damage;

        public ContactPoint Contact => _contact;

        public Transform ObjCollision => _objCollision;

        public CollisionType CollisionType => _collisionType;

        #endregion


        #region StructLifeCycle

        public InfoCollision(float damage, ContactPoint contact, Transform objCollision, Vector3 dir = default, CollisionType collisionType = CollisionType.DamageDealt)
        {
            _damage = damage;
            _dir = dir;
            _contact = contact;
            _objCollision = objCollision;
            _collisionType = collisionType;
        }

        #endregion
    }
}
