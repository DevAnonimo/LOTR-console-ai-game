using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Misc
{
    public abstract class Enumeration : IComparable, IEquatable<Enumeration>
    {
        public string Name { get; }
        public int Id { get; }

        protected Enumeration(int id, string name) => (Id, Name) = (id, name);

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                .Select(f => f.GetValue(null))
                .Cast<T>();

        public override bool Equals(object other)
        {
            return Equals(other as Enumeration);
        }

        public static bool operator ==(Enumeration left, Enumeration right) => left is { } && left.Equals(right);

        public static bool operator !=(Enumeration left, Enumeration right) => !(left == right);

        public virtual bool Equals(Enumeration other)
        {
            return DefaultReferenceComparison(other) &&
                   Name == other!.Name &&
                   Id == other.Id;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ Id;
            }
        }

        public int CompareTo(object other) => Id.CompareTo(((Enumeration) other).Id);

        private bool DefaultReferenceComparison(object other)
        {
            return !ReferenceEquals(null, other) && ReferenceEquals(this, other);
        }
    }
}
