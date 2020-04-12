using UnityEditor;
using UnityEngine;


namespace FirstShooter.Editor
{
    [CustomEditor(typeof(PlayerHealth))]
    public class PlayerHealthEditor : UnityEditor.Editor
    {
        #region Fields 

        private const int MAX_PLAYER_HP = 100;
        private int _hp;
        private int _maxHp = MAX_PLAYER_HP;
        
        #endregion
        
        
        #region UnityMethods

        public override void OnInspectorGUI()
        {
            if (Application.isPlaying)
            {
                DrawDefaultInspector();
            }
            else
            {
                var playerHealthTarget = (PlayerHealth) target;

                _maxHp = EditorGUILayout.IntSlider("Max player hp", _maxHp, 1, MAX_PLAYER_HP);
                _hp = EditorGUILayout.IntSlider("Start player hp", _hp, 1, _maxHp);

                var isButtonPressed = GUILayout.Button("Generate random player hp");

                if (isButtonPressed)
                {
                    _maxHp = Random.Range(1, MAX_PLAYER_HP);
                    _hp = Random.Range(1, _maxHp);
                }

                playerHealthTarget.MaxHp = _maxHp;
                playerHealthTarget.Hp = _hp;
            }
        }

        #endregion

    }
}