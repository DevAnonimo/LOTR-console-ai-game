using UnityEngine;
using static LOTR_LowPoly.Sauron.SauronAnimatorVariableNamesConstants;

namespace LOTR_LowPoly.Sauron
{
    public sealed class SauronBehaviour : BaseBehaviour
    {
        private sealed class SauronPossibleStates : EnemyPossibleState
        {
            public static readonly SauronPossibleStates Attack = new SauronPossibleStates(1, "Attack", IsAttacking);

            public static readonly SauronPossibleStates SpawningEnemies =
                new SauronPossibleStates(2, "SpawningEnemies", SpawnEnemies);

            public static readonly SauronPossibleStates Stun = new SauronPossibleStates(3, "Stun", IsStunning);

            private SauronPossibleStates(int id, string name, string animatorVariableName)
                : base(id, name, animatorVariableName)
            {
            }
        }

        public float attackCooldown = 5f;

        private float _canAttack = -1f;

        protected override void Start()
        {
            base.Start();

            _canAttack = Time.time + attackCooldown;

            OnEnemyGetsNear += AttackPlayer;
            OnEnemyGetsDistant += () => ChangeState(EnemyPossibleState.Seek);
        }

        private void AttackPlayer()
        {
            if (!(_canAttack < Time.time))
            {
                ChangeState(EnemyPossibleState.BattleStance);
                return;
            }

            if (animator.GetBool(IsAttacking))
                return;

            ChangeState(SauronPossibleStates.Attack);
            /*TODO: Attack Player*/
            _canAttack += attackCooldown;
        }
    }
}
