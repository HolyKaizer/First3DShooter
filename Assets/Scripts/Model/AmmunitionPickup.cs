using UnityEngine;


namespace FirstShooter
{
    [RequireComponent(typeof(Collider))]
    public sealed class AmmunitionPickup : BaseObjectScene
    {
        #region Fields

        [SerializeField] private AmmunitionType _ammunitionType;
        [SerializeField] private Clip[] _countOfClipsToAdd;

        #endregion
        
        
        #region UnityMethods

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag(StringManager.PLAYER_TAG))
            {
                ServiceLocator.Resolve<Inventory>().AddClipsToWeapon(_ammunitionType, _countOfClipsToAdd);
                Destroy(gameObject);
            }
        }

        #endregion
    }
}