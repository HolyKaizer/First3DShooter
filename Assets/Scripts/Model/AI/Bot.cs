using System;
using UnityEngine;
using UnityEngine.AI;


namespace FirstShooter
{
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class Bot : BaseObjectScene, IExecute
    {
        #region Fields

        public event Action<Bot> OnDieChange;

        public float Hp = 100;
        public Vision Vision;
        public Weapon Weapon; //TODO: with different weapons

        [SerializeField] private float _timeToDestroy = 10.0f;

        private float _waitTime = 3;
        private StateBot _stateBot;
        private Vector3 _point;
        private float _stoppingDistance = 2.0f;
        private ITimeRemaining _inspectionTimeRemaining;

        #endregion


        #region Properties

        public Transform Target { get; set; }
        public NavMeshAgent Agent { get; private set; }

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

            Agent = GetComponent<NavMeshAgent>();
            _inspectionTimeRemaining = new TimeRemaining(ResetStateBot, _waitTime);
        }

        private void OnEnable()
        {
            var bodyBot = GetComponentInChildren<BodyBot>();
            if (bodyBot != null) bodyBot.OnApplyDamageChange += CollisionEnter;

            var headBot = GetComponentInChildren<HeadBot>();
            if (headBot != null) headBot.OnApplyDamageChange += CollisionEnter;

        }

        private void OnDisable()
        {
            var bodyBot = GetComponentInChildren<BodyBot>();
            if (bodyBot != null) bodyBot.OnApplyDamageChange -= CollisionEnter;

            var headBot = GetComponentInChildren<HeadBot>();
            if (headBot != null) headBot.OnApplyDamageChange -= CollisionEnter;
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
                            _point = Patrol.GenericPoint(transform);
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
                    Weapon.Fire();
                }
                else
                {
                    MovePoint(Target.position);
                }

                //todo - Потеря персонажа 
            }
        }

        #endregion


        #region Methods

        public void CollisionEnter(InfoCollision collisionInfo)
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