using UnityEngine;

namespace FirstShooter
{
    public class Test : MonoBehaviour
    {
        private void Start()
        {
            FindObjectOfType<FlashLightModel>().Layer = 2;
        }
    }
}
