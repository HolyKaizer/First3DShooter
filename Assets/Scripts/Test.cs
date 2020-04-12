using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Random = UnityEngine.Random;


namespace FirstShooter.Editor
{
    public sealed class TestWindow : EditorWindow
    {
        private void OnGUI()
        {
            var isButtonPressed = GUILayout.Button("Spawn enemy");
        }
        
    }
}
