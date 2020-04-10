using System.Linq;
using UnityEngine;
using UnityEngine.AI;


namespace FirstShooter
{
    public sealed class Patrol
    {
        #region Fields
        
        private readonly PatrolPoint[] _listPoint;
        private int _indexCurPoint;

        #endregion 


        #region ClassLifeCycle

        public Patrol(int pathIndex)
        {
            var tempPoints = Object.FindObjectsOfType<PatrolPoint>();
            _listPoint = tempPoints.Where( point => point.PatrolBotIndex == pathIndex).ToArray();
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