using System;
using UnityEngine;
using UnityEngine.AI;


namespace FirstShooter
{
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class Bot : BaseObjectScene, IExecute, ICollision
    {
        #region Fields

        public event Action<Bot> OnDieChange;

        public Vision Vision;
        public Weapon Weapon;
        public float Hp = 100;

        [SerializeField] private float _timeToDestroy = 10.0f;
        
        private Patrol _patrol; 
        private Vector3 _point;
        private ITimeRemaining _inspectionTimeRemaining;
        private StateBot _stateBot;
        
        private float _waitOnPointTime = 3;
        private float _stoppingDistance = 2.0f;
        private int _pathIndex;

        #endregion


        #region Properties

        public Transform Target { get; set; }
        public NavMeshAgent Agent { get; private set; }

        public int PathIndex
        {
            get => _pathIndex;
            set
            {
                _pathIndex = value;
                _patrol = new Patrol(value);
            }
        }

        private StateBot StateBot
        {
            get => _stateBot;

            set
            {
                _stateBot = value;
                switch (value)
                {
                    case StateBot.None:
                        Color = Color.white;
                        break;
                    case StateBot.Patrol:
                        Color = Color.green;
                        break;
                    case StateBot.Inspection:
                        Color = Color.yellow;
                        break;
                    case StateBot.Detected:
                        Color = Color.red;
                        break;
                    case StateBot.Died:
                        Color = Color.gray;
                        break;
                    default:
                        Color = Color.white;
                        break;
                }
            }
        }

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();

            _patrol = new Patrol(PathIndex);
            Agent = GetComponent<NavMeshAgent>();
            _inspectionTimeRemaining = new TimeRemaining(ResetStateBot, _waitOnPointTime);
        }

        private void OnEnable()
        {
            var bodyBot = GetComponentInChildren<BodyBot>();
            if (bodyBot != null)
            {
                bodyBot.OnApplyDamageChange += CollisionEnter;
                bodyBot.OnHealingChange += CollisionEnter;
            }

            var headBot = GetComponentInChildren<HeadBot>();
            if (headBot != null)
            {
                headBot.OnApplyDamageChange += CollisionEnter;
                headBot.OnHealingChange += CollisionEnter;
            }

        }

        private void OnDisable()
        {
            var bodyBot = GetComponentInChildren<BodyBot>();
            if (bodyBot != null)
            {
                bodyBot.OnApplyDamageChange -= CollisionEnter;
                bodyBot.OnHealingChange -= CollisionEnter;
            }

            var headBot = GetComponentInChildren<HeadBot>();
            if (headBot != null)
            {
                headBot.OnApplyDamageChange -= CollisionEnter;
                headBot.OnHealingChange -= CollisionEnter;
            }
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (StateBot == StateBot.Died) return;

            if (StateBot != StateBot.Detected)
            {
                if (!Agent.hasPath)
                {
                    if (StateBot != StateBot.Inspection)
                    {
                        if (StateBot != StateBot.Patrol)
                        {
                            StateBot = StateBot.Patrol;
                            _point = _patrol.GetNextPointInPatrolPath();
                            MovePoint(_point);
                        }
                        else
                        {
                            if ((_point - transform.position).sqrMagnitude <= 1.0f)
                            {
                                StateBot = StateBot.Inspection;
                                _inspectionTimeRemaining.AddTimeRemaining();
                            }
                        }
                    }
                }

                if (Vision.VisionM(transform, Target))
                {
                    StateBot = StateBot.Detected;
                }
            }
            else
            {
                if (Math.Abs(Agent.stoppingDistance - _stoppingDistance) > Mathf.Epsilon)
                {
                    Agent.stoppingDistance = _stoppingDistance;
                }

                if (Vision.VisionM(transform, Target))
                {
                    _point = Target.position;
                    MovePoint(_point);
                    Weapon.Fire();
                }
                else
                {
                    Agent.stoppingDistance = 0.0f;
                    MovePoint(transform.position);
                    StateBot = StateBot.Inspection;
                    _inspectionTimeRemaining.AddTimeRemaining();
                }
            }
        }

        #endregion


        #region Methods

        public void CollisionEnter(InfoCollision collisionInfo)
        {
            if (collisionInfo.CollisionType == CollisionType.DamageDealt)
            {
                if (Hp > 0)
                {
                    Hp -= collisionInfo.Damage;
                }

                if (Hp <= 0)
                {
                    Die(collisionInfo);
                }
            }
            else if (collisionInfo.CollisionType == CollisionType.Healing)
            {
                Hp += collisionInfo.Damage;
            }
        }

        private void Die(InfoCollision info)
        {
            StateBot = StateBot.Died;
            Agent.enabled = false;

            foreach (Transform child in GetComponentInChildren<Transform>())
            {
                child.parent = null;

                var tempRbChild = child.GetComponent<Rigidbody>();
                if (!tempRbChild)
                {
                    tempRbChild = child.gameObject.AddComponent<Rigidbody>();
                }
                tempRbChild.AddForce(info.Dir * UnityEngine.Random.Range(20, 100));

                Destroy(child.gameObject, _timeToDestroy);
            }

            OnDieChange?.Invoke(this);
        }

        private void ResetStateBot()
        {
            StateBot = StateBot.None;
        }

        public void MovePoint(Vector3 point)
        {
            Agent.SetDestination(point);
        }

        #endregion
    }
}