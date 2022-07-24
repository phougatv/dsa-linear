using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Dsa.Linear.UnitTests")]
namespace Dsa.Linear.LinkedList.Doubly;
public class Node : IEquatable<Node>, IComparable<Node>
{
	public Int32 Key { get; private set; }
	public Node Next { get; internal set; }
	public Node Previous { get; internal set; }

	internal Node(Int32 key)
	{
		Key = key;
		Next = Previous = null!;
	}

	internal void Clear() => Next = Previous = null!;

	#region IComparable<Node>.CompareTo Override
	public Int32 CompareTo(Node? node)
	{
		ArgumentNullException.ThrowIfNull(node, nameof(node));
		return Comparer<Int32>.Default.Compare(Key, node.Key);
	}
	#endregion IComparable<Node>.CompareTo Override

	#region Object.Equals and Object.GetHashCode Override
	public override Boolean Equals(Object? obj) => obj is Node node && Equals(node);
	public override Int32 GetHashCode() => HashCode.Combine(Key);
	#endregion Object.Equals and Object.GetHashCode Override

	#region IEquatable<Node>.Equals Override
	public Boolean Equals(Node? node) => this == node!;
	#endregion IEquatable<Node>.Equals Override

	#region Operator Overloading
	public static Boolean operator ==(Node left, Node right)
	{
		if (ReferenceEquals(left, right)) return true;
		if (left is null) return false;
		if (right is null) return false;
		return left.Key == right.Key;
	}
	public static Boolean operator !=(Node left, Node right) => !(left == right);
	#endregion Operator Overloading
}
