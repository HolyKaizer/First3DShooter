using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Random = UnityEngine.Random;


namespace FirstShooter.Editor
{
    public sealed class EnemySpawnerWindow : EditorWindow
    {
        #region Fields

        public GameObject EnemyPrefab;
        public GameObject PatrolPoint;
        
        private Vector3 _minPatrolPoint;
        private Vector3 _maxPatrolPoint;
        private int _patrolPointsCount;
        private int _patrolIndex;
        

        #endregion
        
        
        #region UnityMethods

        private void OnGUI()
        {
            GUILayout.Label("Prefab settings", EditorStyles.boldLabel);
            EnemyPrefab = (GameObject) EditorGUILayout.ObjectField("Enemy prefab", EnemyPrefab, typeof(GameObject), true);
            PatrolPoint = (GameObject) EditorGUILayout.ObjectField("Patrol point prefab", PatrolPoint, typeof(GameObject), true);

            GUILayout.Label("Enemy patrol path settings", EditorStyles.boldLabel);
            _patrolPointsCount = EditorGUILayout.IntSlider("Count of patrol points", _patrolPointsCount, 2, 20);
            _patrolIndex = EditorGUILayout.IntField("Patrol index", _patrolIndex);

            _minPatrolPoint = EditorGUILayout.Vector3Field("Min patrol point position", _minPatrolPoint);
            _maxPatrolPoint = EditorGUILayout.Vector3Field("Max patrol point position", _maxPatrolPoint);

            var isButtonPressed = GUILayout.Button("Spawn enemy");
            if (isButtonPressed)
            {
                SpawnEnemy();
            }
        }

        #endregion

        
        #region Methods

        [MenuItem("FirstShooter/EnemySpawner")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(EnemySpawnerWindow));
        }

        private void SpawnEnemy()
        {
            var root = new GameObject($"PatrolPath{_patrolIndex}");
            for (int i = 0; i < _patrolPointsCount; i++)
            {
                var positionX = Random.Range(_minPatrolPoint.x, _maxPatrolPoint.x);
                var positionY = Random.Range(_minPatrolPoint.y, _maxPatrolPoint.y);
                var positionZ = Random.Range(_minPatrolPoint.z, _maxPatrolPoint.z);
                var pointPosition = new Vector3(positionX, positionY, positionZ);
                
                var tempPoint = Instantiate(PatrolPoint, pointPosition, Quaternion.identity, root.transform);
                tempPoint.GetComponent<PatrolPoint>().PatrolBotIndex = _patrolIndex;
                tempPoint.name = "PatrolPathPoint";
                SetObjDirty(tempPoint);
            }
            
            var patrol = new Patrol(_patrolIndex);
            var tempBot = Instantiate(EnemyPrefab.GetComponent<Bot>(),patrol.GetNextPointInPatrolPath(),Quaternion.identity);
            
            SetObjDirty(tempBot.gameObject);
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