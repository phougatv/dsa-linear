namespace Dsa.Linear.Generics.LinkedList;
public class Node<T> : IComparable<Node<T>>, IEquatable<Node<T>>
	where T : notnull, IComparable<T>, IEquatable<T>
{
	public T Key { get; private set; }
	public Node<T> Next { get; internal set; } = null!;
	public Node<T> Previous { get; internal set; } = null!;

	internal Node(T key)
	{
		Key = key;
	}

	internal void Clear() => Next = Previous = null!;

	#region IComparable<T>.CompareTo Override
	public Int32 CompareTo(Node<T>? node)
	{
		ArgumentNullException.ThrowIfNull(node, nameof(node));
		return Comparer<T>.Default.Compare(Key, node.Key);
	}
	#endregion IComparable<Node>.CompareTo Override

	#region Object.Equals and Object.GetHashCode Override
	public override Boolean Equals(Object? obj) => obj is Node<T> node && Equals(node);
	public override Int32 GetHashCode() => HashCode.Combine(Key);
	#endregion Object.Equals and Object.GetHashCode Override

	#region IEquatable<Node>.Equals Override
	public Boolean Equals(Node<T>? node) => this == node!;
	#endregion IEquatable<Node>.Equals Override

	#region Operator Overloading
	public static Boolean operator ==(Node<T> left, Node<T> right)
	{
		if (ReferenceEquals(left, right)) return true;
		if (left is null) return false;
		if (right is null) return false;
		if (ReferenceEquals(left.Key, right.Key)) return true;
		if (left.Key is null) return false;
		if (right.Key is null) return false;

		return left.Key.Equals(right.Key);
	}
	public static Boolean operator !=(Node<T> left, Node<T> right) => !(left == right);
	#endregion Operator Overloading
}
