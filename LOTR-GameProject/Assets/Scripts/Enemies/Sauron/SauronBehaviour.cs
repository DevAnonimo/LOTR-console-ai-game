using SimpleEnemy;
using UnityEngine;
using static LOTR_LowPoly.Sauron.SauronAnimatorVariableNamesConstants;

namespace LOTR_LowPoly.Sauron
{
    public sealed class SauronBehaviour : BaseBehaviour
    {
        private sealed class SauronPossibleStates : EnemyPossibleState
        {
            public static readonly SauronPossibleStates SpawningEnemies =
                new SauronPossibleStates(2, "SpawningEnemies", SpawnEnemies);

            public static readonly SauronPossibleStates Stun = new SauronPossibleStates(3, "Stun", IsStunning);

            private SauronPossibleStates(int id, string name, string animatorVariableName)
                : base(id, name, animatorVariableName)
            {
            }
        }
    }
}
