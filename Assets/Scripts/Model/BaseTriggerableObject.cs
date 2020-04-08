using UnityEngine;


namespace FirstShooter
{
    [RequireComponent(typeof(Collider))]
    public abstract class BaseTriggerableObject : BaseObjectScene
    {
        #region PrivateData

        protected struct TriggerInfo
        {
            public readonly float FloatData;
            public readonly CollisionType CollisionType;

            public TriggerInfo(float floatData, CollisionType collisionType)
            {
                FloatData = floatData;
                CollisionType = collisionType;
            }
        }

        #endregion
        
        
        #region Methods

        protected void Trigger(Collider collider, TriggerInfo withInfo)
        {
            if (collider.gameObject.TryGetComponent<ICollision>(out var iCollision))
            {
                var direction = collider.gameObject.transform.position - transform.position;
                var infoCollision = new InfoCollision(withInfo.FloatData, new ContactPoint(), transform, direction, withInfo.CollisionType);
                
                iCollision.CollisionEnter(infoCollision);
                Destroy(gameObject);
            }
        }

        #endregion
        
    }
}