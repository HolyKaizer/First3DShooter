using System.Linq;
using UnityEngine;
using UnityEngine.AI;


namespace FirstShooter
{
    public sealed class Patrol
    {
        #region Fields
        
        private readonly PatrolPoint[] _listPoint;
        private int _indexCurPoint = 0;
        private int _minDistance = 25;
        private int _maxDistance = 150;
        private int _pathIndex = 0;

        #endregion 


        #region ClassLifeCycle

        public Patrol(int pathIndex)
        {
            _pathIndex = pathIndex;
            
            var tempPoints = Object.FindObjectsOfType<PatrolPoint>();
            _listPoint = tempPoints.Where( point => point.PatrolBotIndex == _pathIndex).ToArray();
        }

        #endregion
        
        
        #region Methods

        public Vector3 GetNextPointInPatrolPath()
        {
            if (_indexCurPoint < _listPoint.Length - 1)
            {
                _indexCurPoint++;
            }
            else
            {
                _indexCurPoint = 0;
            }
            var result = _listPoint[_indexCurPoint].transform.position;

            return result;
        }

        #endregion
    }
}