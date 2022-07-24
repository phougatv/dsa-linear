namespace Dsa.Linear.Generics.LinkedList;

using System.Diagnostics.CodeAnalysis;

public class DoublyCircularLinkedList<T>
	where T : IComparable<T>, IEquatable<T>
{
	#region Private Members
	private const Node<T> NullNode = null!;
	#endregion Private Members

	#region Public Properties
	public Int32 Count { get; private set; } = 0;
	public Boolean IsEmpty => Count == 0 && Head is NullNode && Tail is NullNode;
	public Node<T> Head { get; private set; } = NullNode;
	public Node<T> Tail => Head?.Previous!;
	#endregion Public Properties

	#region Public Ctors
	public DoublyCircularLinkedList()
	{ }
	public DoublyCircularLinkedList(IEnumerable<T> collection)
	{
		ArgumentNullException.ThrowIfNull(collection, nameof(collection));
		var enumerator = collection.GetEnumerator();
		while (enumerator.MoveNext())
			AddTail(enumerator.Current);

	}
	#endregion Public Ctors

	#region Public APIs
	/// <summary>
	/// Adds <paramref name="item"/> to the end of the linked list.
	/// </summary>
	/// <param name="item">The item</param>
	public void Add(T item) => AddTail(item);

	/// <summary>
	/// Adds <paramref name="item"/> to the start of the linked list.
	/// </summary>
	/// <param name="item">The item</param>
	public void AddHead(T item)
	{
		var node = GetNewNode(item);
		InternalAddHead(node);
		IncreaseCountBy1();
	}

	/// <summary>
	/// Adds item to the end of the linked list.
	/// </summary>
	/// <param name="item">The item</param>
	public void AddTail(T item)
	{
		var node = GetNewNode(item);
		InternalAddTail(node);
		IncreaseCountBy1();
	}

	/// <summary>
	/// Clears the entire linked list.
	/// </summary>
	public void Clear()
	{
		if (IsEmpty) return;

		var current = Head;
		while (current is not NullNode)
		{
			var temp = current;
			current = current.Next;
			temp.Clear();
		}

		Head = NullNode;
		Count = 0;
	}

	/// <summary>
	/// Checks if the item is exists in the list
	/// </summary>
	/// <param name="item">The item</param>
	/// <returns>true if <paramref name="item"/> exists, false otherwise</returns>
	public Boolean Contains(T item) => Find(item) is not NullNode;

	/// <summary>
	/// Gets the node of the list whose key matches the item, null otherwise.
	/// </summary>
	/// <param name="item">The item</param>
	/// <returns>The <see cref="Node{T}"/></returns>
	public Node<T> Find(T item)
	{
		//if (item is null) return NullNode;
		if (IsEmpty) return NullNode;

		var itemNode = new Node<T>(item);
		if (Head == itemNode) return Head;

		var current = Head.Next;
		while (!ReferenceEquals(Head, current) && current != itemNode)
			current = current.Next;
		if (ReferenceEquals(Head, current)) return NullNode;
		return current;
	}

	/// <summary>
	/// Removes the node contains the <paramref name="item"/> from the linked list.
	/// </summary>
	/// <param name="item">The item</param>
	public void Remove(T item)
	{
		if (IsEmpty) return;

		var node = Find(item);
		if (node is NullNode) return;
		InternalRemove(node);
		DecreaseCountBy1();
	}

	/// <summary>
	/// Removes the head node of the linked list and make reference corrections
	/// </summary>
	public void RemoveHead()
	{
		if (IsEmpty) return;
		InternalRemove(Head);
		DecreaseCountBy1();
	}

	/// <summary>
	/// Removes the tail node of the linked list and make reference corrections
	/// </summary>
	public void RemoveTail()
	{
		if (IsEmpty) return;
		InternalRemove(Tail);
		DecreaseCountBy1();
	}
	#endregion Public APIs

	#region Private Methods
	/// <summary>
	/// Decreases Count by 1
	/// </summary>
	private void DecreaseCountBy1() => --Count;

	/// <summary>
	/// Increases Count by 1
	/// </summary>
	private void IncreaseCountBy1() => ++Count;

	/// <summary>
	/// Gets node instance with <paramref name="item"/> as its Key.
	/// </summary>
	/// <param name="item">The item</param>
	/// <returns>An instance of <see cref="Node{T}"/></returns>
	private static Node<T> GetNewNode(T item) => new Node<T>(item);

	/// <summary>
	/// Adds <paramref name="node"/> as the head of the list.
	/// For private use only.
	/// </summary>
	/// <param name="node">The <see cref="Node{T}"/></param>
	private void InternalAddHead(Node<T> node)
	{
		ArgumentNullException.ThrowIfNull(node, nameof(node));
		if (IsEmpty)
		{
			InternalAddNodeWhenListIsEmpty(node);
			return;
		}

		node.Next = Head;
		node.Previous = Tail;
		Head.Previous = node;
		Head = node;
	}

	/// <summary>
	/// Adds <paramref name="node"/> as the tail of the list.
	/// For private use only.
	/// </summary>
	/// <param name="node">The <see cref="Node{T}"/></param>
	private void InternalAddTail(Node<T> node)
	{
		ArgumentNullException.ThrowIfNull(node, nameof(node));
		if (IsEmpty)
		{
			InternalAddNodeWhenListIsEmpty(node);
			return;
		}

		node.Previous = Tail;
		node.Next = Head;
		Head.Previous = Tail.Next = node;
	}

	/// <summary>
	/// Adds a node when the list is empty.
	/// NOTE: Considers 'node' valid and not null. For 'private' use only
	/// </summary>
	/// <param name="node">The <see cref="Node{T}"/></param>
	private void InternalAddNodeWhenListIsEmpty(Node<T> node)
	{
		Head = node;
		node.Next = Head;
		node.Previous = Head;
	}

	/// <summary>
	/// Removes the <paramref name="node"/> from the list and makes necessary reference corrections.
	/// For private use only.
	/// </summary>
	/// <param name="node">The <see cref="Node{T}"/></param>
	private void InternalRemove(Node<T> node)
	{
		if (node is NullNode) return;
		if (Count == 1)
		{
			node.Next = node.Previous = NullNode;
			Head = NullNode;
			return;
		}

		//Detach the 'node' from the linked list
		if (node.Previous is not NullNode) node.Previous.Next = node.Next;
		if (node.Next is not NullNode) node.Next.Previous = node.Previous!;

		//If required make Head reference corrections
		if (IsHead(node)) Head = node.Next!;

		node.Next = node.Previous = NullNode;
	}

	/// <summary>
	/// Checks if the <paramref name="node"/> is Head of the linked list.
	/// </summary>
	/// <param name="node">The node.</param>
	/// <returns>True if the node is Head node, false otherwise.</returns>
	private Boolean IsHead([NotNull] Node<T> node)
	{
		ArgumentNullException.ThrowIfNull(node, nameof(node));
		return Head == node;
	}
	#endregion Private Methods
}
