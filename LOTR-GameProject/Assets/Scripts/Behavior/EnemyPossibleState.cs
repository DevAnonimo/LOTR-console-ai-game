using System;
using Scripts.Misc;
using UnityEngine;
using static Scripts.AnimatorVariableNamesConstants;

namespace Scripts.Behavior
{
    public partial class BaseBehaviour
    {
        public class EnemyPossibleState : Enumeration, IEquatable<EnemyPossibleState>
        {
            public int AnimationHash { get; }

            public static readonly EnemyPossibleState Idle = new EnemyPossibleState(1, "Idle", IsRunning);
            public static readonly EnemyPossibleState Seek = new EnemyPossibleState(2, "Seek", IsRunning);
            public static readonly EnemyPossibleState BattleStance = new EnemyPossibleState(3, "BattleStance", IsRunning);
            public static readonly EnemyPossibleState Attack = new EnemyPossibleState(4, "Attack", IsAttacking);

            protected EnemyPossibleState(int id, string name, string animatorVariableName) : base(id, name)
                => AnimationHash = Animator.StringToHash(animatorVariableName);

            public static bool operator ==(EnemyPossibleState left, EnemyPossibleState right) => left is { } && left.Equals(right);

            public static bool operator !=(EnemyPossibleState left, EnemyPossibleState right) => !(left == right);

            public bool Equals(EnemyPossibleState other)
            {
                return base.Equals(other) && AnimationHash == other.AnimationHash;
            }

            public override bool Equals(object obj)
            {
                return base.Equals(obj) && Equals((EnemyPossibleState) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (base.GetHashCode() * 397) ^ AnimationHash;
                }
            }
        }
    }
}
