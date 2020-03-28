using UnityEngine;


namespace FirstShooter
{
    public sealed class Wall : BaseObjectScene, ISelectedObj
    {
        #region ISelectedObj

        public string GetMessage()
        {
            return gameObject.name;
        }

        #endregion
    }
}

