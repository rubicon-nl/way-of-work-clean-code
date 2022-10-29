namespace Rubicon.Wow.CleanCode.Data;

public class EquatableHashSet<T> : HashSet<T>
{
    public EquatableHashSet() : base() { }

    public EquatableHashSet(IEnumerable<T> collection) : base(collection) { }

    public override bool Equals(object? obj)
    {
        if (obj is IEnumerable<T> other)
        {
            // use set equality by default when possible
            return this.SetEquals(other);
        }

        return base.Equals(obj);
    }
}