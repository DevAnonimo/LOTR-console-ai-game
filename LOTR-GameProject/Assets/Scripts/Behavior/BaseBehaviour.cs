using System.Linq;
using Scripts.Misc;
using Scripts.Player;
using Scripts.SimpleEnemy;
using UnityEngine;
using static Scripts.AnimatorVariableNamesConstants;

namespace Scripts.Behavior
{
    public abstract partial class BaseBehaviour : MonoBehaviour
    {
        private float _currentSpeed;
        private float _canAttack = -1f;
        private EnemyPossibleState _currentState;
        private CharacterCombat _playerCombatController;

        public delegate void EnemyGetsNear();

        public delegate void EnemyGetsDistant();

        public event EnemyGetsNear OnEnemyGetsNear = () => { };
        public event EnemyGetsDistant OnEnemyGetsDistant = () => { };

        public float maxSpeed;
        public float distanceToAttack;
        public float distanceToBeginArrival;
        public float attackCooldown = 5f;
        public int attackDamage = 15;

        public SeekState seekState;

        public SeekBehaviour seekBehaviour;

        public BaseAgent agent;
        public GameObject target;
        public Animator animator;
        public CombatAreaDoorTrigger combatAreaDoorTrigger;

        protected virtual void Start()
        {
            _canAttack = Time.time + attackCooldown;
            _playerCombatController = target.GetComponent<CharacterCombat>();

            OnEnemyGetsNear += AttackPlayer;
            OnEnemyGetsDistant += () => ChangeState(EnemyPossibleState.Seek);
            combatAreaDoorTrigger.OnPlayerCrossBattleTrigger += () => ChangeState(EnemyPossibleState.Seek);

            agent = gameObject.AddComponent<BaseAgent>();
            ChangeState(EnemyPossibleState.Idle);
            agent.speed = maxSpeed;
            _currentSpeed = maxSpeed;
        }

        private void FixedUpdate()
        {
            ControlMovingBehaviour();
        }

        protected void ChangeState(EnemyPossibleState newState)
        {
            _currentState = newState;

            if (newState == EnemyPossibleState.Idle)
            {
                if (seekState) DestroyImmediate(seekState);
            }
            else if (newState == EnemyPossibleState.Seek)
            {
                if (gameObject.GetComponent<SeekState>() is { } == false)
                    seekState = gameObject.AddComponent<SeekState>();
            }

            UpdateCurrentAnimation(newState);
        }

        private void UpdateCurrentAnimation<T>(T currentState) where T : EnemyPossibleState
        {
            if (animator is { } == false)
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

            if (distance > distanceToBeginArrival && _currentSpeed <= maxSpeed &&
                _currentState != EnemyPossibleState.Idle)
            {
                _currentSpeed += maxSpeed * Time.deltaTime;

                OnEnemyGetsDistant();
            }

            agent.speed = _currentSpeed;
        }

        protected virtual void AttackPlayer()
        {
            if (_canAttack < Time.time == false)
            {
                ChangeState(EnemyPossibleState.BattleStance);
                return;
            }

            if (animator.GetBool(IsAttacking))
                return;

            ChangeState(EnemyPossibleState.Attack);
            _playerCombatController.TakeDamage(attackDamage);
            _canAttack += attackCooldown;
        }
    }
}
