using UnityEngine;


namespace FirstShooter
{
    public sealed class GameController : MonoBehaviour
    {
        #region Fields

        public GameplayData GameplayData;

        private Controllers _controllers;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _controllers = new Controllers();
            _controllers.Initialization();
        }

        private void Update()
        {
            for (var i = 0; i < _controllers.Length; i++)
            {
                _controllers[i].Execute();
            }
        }

        #endregion
    }
}
