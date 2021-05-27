using System;
using DefaultNamespace;
using Misc;
using UnityEngine;

namespace LOTR_LowPoly
{
    public partial class BaseBehaviour
    {
        public class EnemyPossibleState : Enumeration, IEquatable<EnemyPossibleState>
        {
            public int AnimationHash { get; }

            public static readonly EnemyPossibleState Idle = new EnemyPossibleState(1, "Idle", AnimatorVariableNamesConstants.IsRunning);
            public static readonly EnemyPossibleState Seek = new EnemyPossibleState(2, "Seek", AnimatorVariableNamesConstants.IsRunning);
            public static readonly EnemyPossibleState BattleStance = new EnemyPossibleState(3, "BattleStance", AnimatorVariableNamesConstants.IsRunning);

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
