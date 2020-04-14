using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Random = UnityEngine.Random;


namespace FirstShooter.Editor
{ 
    public sealed class MedKitSpawnerWindow : EditorWindow
    {
        #region Fields

        private GameObject _medkitPrefab;
        private Vector3 _minPositionPoint;
        private Vector3 _maxPositionPoint;
        
        private int _medKitCount;

        #endregion
        
        
        #region UnityMethods

        private void OnEnable()
        {
            _medkitPrefab = Resources.Load<GameObject>(Path.Combine(StringManager.PREFAB_FOLDER_NAME,
                                                                        StringManager.MEDKIT_PREFAB_NAME));
        }

        private void OnGUI()
        {
            GUILayout.Label("Medkit position settings", EditorStyles.boldLabel);
            _medKitCount = EditorGUILayout.IntSlider("Count of medkits", _medKitCount, 1, 20);

            _minPositionPoint = EditorGUILayout.Vector3Field("Min medkit position", _minPositionPoint);
            _maxPositionPoint = EditorGUILayout.Vector3Field("Max medkit position", _maxPositionPoint);

            var isButtonPressed = GUILayout.Button("Spawn medkits");
            if (isButtonPressed)
            {
                SpawnMedkit();
            }
        }

        #endregion


        #region Methods

        [MenuItem("FirstShooter/MedKitSpawner")]
        public static void ShowWindw()
        {
            GetWindow(typeof(MedKitSpawnerWindow));
        }
        
        private void SpawnMedkit()
        {
            var root = new GameObject($"Medkits");
            for (int i = 0; i < _medKitCount; i++)
            {
                var positionX = Random.Range(_minPositionPoint.x, _maxPositionPoint.x);
                var positionY = Random.Range(_minPositionPoint.y, _maxPositionPoint.y);
                var positionZ = Random.Range(_minPositionPoint.z, _maxPositionPoint.z);
                var medkitPosition = new Vector3(positionX, positionY, positionZ);
                
                var tempMedkit = Instantiate(_medkitPrefab, medkitPosition, Quaternion.identity, root.transform);
                tempMedkit.name = "Medkit";
                
                SetObjDirty(tempMedkit);
            }
        }
        
        private void SetObjDirty(GameObject obj)
        {
            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(obj);
                EditorSceneManager.MarkSceneDirty(obj.scene);
            } 
        } 
        
        #endregion
    }
}