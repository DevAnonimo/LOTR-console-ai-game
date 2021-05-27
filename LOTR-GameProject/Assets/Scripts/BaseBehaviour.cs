using System.Linq;
using Misc;
using UnityEngine;

namespace LOTR_LowPoly
{
    public abstract partial class BaseBehaviour : MonoBehaviour
    {
        private float _currentSpeed;
        private EnemyPossibleState _currentState;

        public delegate void EnemyGetsNear();
        public delegate void EnemyGetsDistant();

        public event EnemyGetsNear OnEnemyGetsNear = () => { };
        public event EnemyGetsDistant OnEnemyGetsDistant = () => { };

        public float maxSpeed;
        public float distanceToAttack;
        public float distanceToBeginArrival;

        public SeekState seekState;

        public SeekBehaviour seekBehaviour;

        public BaseAgent agent;
        public GameObject target;
        public Animator animator;
        public CombatAreaDoorTrigger combatAreaDoorTrigger;

        protected virtual void Start()
        {
            combatAreaDoorTrigger.OnPlayerCrossBattleTrigger += player =>
            {
                target = player;
                ChangeState(EnemyPossibleState.Seek);
            };

            agent = gameObject.AddComponent<BaseAgent>();
            ChangeState(EnemyPossibleState.Idle);
            agent.speed = maxSpeed;
            _currentSpeed = maxSpeed;
            _currentSpeed = maxSpeed;
        }

        private void FixedUpdate()
        {
            ControlMovingBehaviour();
        }

        protected void ChangeState(EnemyPossibleState newState)
        {
            //if (_currentState == newState) return;

            _currentState = newState;

            if (newState == EnemyPossibleState.Idle)
            {
                if (seekState != null) DestroyImmediate(seekState);
            }
            else if (newState == EnemyPossibleState.Seek)
            {
                if (gameObject.GetComponent<SeekState>() == null)
                    seekState = gameObject.AddComponent<SeekState>();
            }

            UpdateCurrentAnimation(newState);
        }

        private void UpdateCurrentAnimation<T>(T currentState) where T : EnemyPossibleState
        {
            if (!(animator is { }))
                return;

            var enemyPossibleStates = Enumeration.GetAll<T>();

            if (currentState == EnemyPossibleState.Idle || currentState == EnemyPossibleState.BattleStance)
            {
                foreach (var possibleStates in enemyPossibleStates)
                {
                    animator.SetBool(possibleStates.AnimationHash, false);
                }

                return;
            }

            var notRunningAnimations = enemyPossibleStates.Where(s => s != currentState);

            foreach (var possibleStates in notRunningAnimations)
            {
                animator.SetBool(possibleStates.AnimationHash, false);
            }

            animator.SetBool(currentState.AnimationHash, true);
        }

        private void ControlMovingBehaviour()
        {
            var distance = Vector3.Distance(target.transform.position, transform.position);

            if (distance < distanceToBeginArrival)
            {
                _currentSpeed -= _currentSpeed * Time.deltaTime;

                if (distance < 1.5f)
                    _currentSpeed = 0f;

                OnEnemyGetsNear();
            }

            if (distance > distanceToBeginArrival && _currentSpeed <= maxSpeed && _currentState != EnemyPossibleState.Idle)
            {
                _currentSpeed += maxSpeed * Time.deltaTime;

                OnEnemyGetsDistant();
            }

            agent.speed = _currentSpeed;
        }
    }
}
