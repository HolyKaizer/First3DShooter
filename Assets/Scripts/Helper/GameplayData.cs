using System;
using UnityEngine;


namespace FirstShooter
{
    [Serializable]
    public sealed class GameplayData
    {
        public LayerMask NothingLayerMask;
        public LayerMask PlayerLayerMask;
        public LayerMask SelecatableLayerMask;
    }
}