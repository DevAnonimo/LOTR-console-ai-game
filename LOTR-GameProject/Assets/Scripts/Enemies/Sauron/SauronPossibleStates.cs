namespace Scripts.Enemies.Sauron
{
    public sealed partial class SauronBehaviour
    {
        private sealed class SauronPossibleStates : EnemyPossibleState
        {
            public static readonly SauronPossibleStates SpawningEnemies =
                new SauronPossibleStates(2, "SpawningEnemies", SauronAnimatorVariableNamesConstants.SpawnEnemies);

            public static readonly SauronPossibleStates Stun =
                new SauronPossibleStates(3, "Stun", SauronAnimatorVariableNamesConstants.IsStunning);

            private SauronPossibleStates(int id, string name, string animatorVariableName)
                : base(id, name, animatorVariableName)
            {
            }
        }
    }
}
