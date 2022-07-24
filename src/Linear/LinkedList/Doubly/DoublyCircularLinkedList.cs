namespace Dsa.Linear.LinkedList.Doubly;

using System.Diagnostics.CodeAnalysis;

public class DoublyCircularLinkedList
{
	#region Private Members
	private const Node NullNode = null!;
	#endregion Private Members

	#region Public Properties
	/// <summary>
	/// The count of the linked list.
	/// </summary>
	public Int32 Count { get; private set; } = 0;

	/// <summary>
	/// Checks if the linked list is empty.
	/// </summary>
	public Boolean IsEmpty => Count == 0 && Head is NullNode;

	/// <summary>
	/// Head of the linked list.
	/// </summary>
	public Node Head { get; private set; } = NullNode;

	/// <summary>
	/// Tail of the linked list.
	/// </summary>
	public Node Tail => Head?.Previous!;
	#endregion Public Properties

	#region Public Ctors
	public DoublyCircularLinkedList()
	{ }

	public DoublyCircularLinkedList(IEnumerable<Int32> collection)
	{
		ArgumentNullException.ThrowIfNull(collection, nameof(collection));
		var enumerator = collection.GetEnumerator();
		while (enumerator.MoveNext())
		{
			AddTail(enumerator.Current);
		}
	}
	#endregion Public Ctors

	#region Public APIs
	/// <summary>
	/// Adds <paramref name="item"/> to the end of the linked list.
	/// </summary>
	/// <param name="item">The item</param>
	public void Add(Int32 item) => AddTail(item);

	/// <summary>
	/// Adds <paramref name="item"/> to the start of the linked list.
	/// </summary>
	/// <param name="item">The item</param>
	public void AddHead(Int32 item)
	{
		var node = GetNewNode(item);
		InternalAddHead(node);
		IncreaseCountBy1();
	}

	/// <summary>
	/// Adds <paramref name="item"/> to the end of the linked list.
	/// </summary>
	/// <param name="item">The item</param>
	public void AddTail(Int32 item)
	{
		var node = GetNewNode(item);
		InternalAddTail(node);
		IncreaseCountBy1();
	}

	/// <summary>
	/// Removes all the items from the linked list.
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
	/// Checks if the <paramref name="item"/> exists in the linked list.
	/// </summary>
	/// <param name="item">The item</param>
	/// <returns>True if <paramref name="item"/> exists, false otherwise.</returns>
	public Boolean Contains(Int32 item) => Find(item) is not NullNode;

	/// <summary>
	/// Finds the <paramref name="item"/>.
	/// </summary>
	/// <param name="item">The item</param>
	/// <returns>Node containing the <paramref name="item"/>, null if the <paramref name="item"/> is not found.</returns>
	public Node Find(Int32 item)
	{
		if (IsEmpty) return NullNode;
		if (Head.Key == item) return Head;

		var current = Head.Next;
		while (!ReferenceEquals(Head, current) && current.Key != item)
			current = current.Next;
		if(ReferenceEquals(current, Head)) return NullNode;
		return current;
	}

	/// <summary>
	/// Removes the <paramref name="item"/> from the linked list.
	/// </summary>
	/// <param name="item">The item</param>
	public void Remove(Int32 item)
	{
		if (IsEmpty) return;

		var node = Find(item);
		if (node is NullNode) return;
		InternalRemove(node);
		DecreaseCountBy1();
	}

	/// <summary>
	/// Removes the head of the linked list and makes the necessary reference adjustments.
	/// </summary>
	public void RemoveHead()
	{
		if (IsEmpty) return;
		InternalRemove(Head);
		DecreaseCountBy1();
	}

	/// <summary>
	/// Removes the tail of the linked list and makes the necessary reference adjustments.
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
	/// <returns>An instance of <see cref="Node"/></returns>
	private static Node GetNewNode(Int32 item) => new Node(item);

	/// <summary>
	/// Adds <paramref name="node"/> as the head of the list.
	/// For private use only.
	/// </summary>
	/// <param name="node">The <see cref="Node"/></param>
	private void InternalAddHead(Node node)
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
	/// <param name="node">The <see cref="Node"/></param>
	private void InternalAddTail(Node node)
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
	/// <param name="node">The <see cref="Node"/></param>
	private void InternalAddNodeWhenListIsEmpty(Node node)
	{
		Head = node;
		node.Next = Head;
		node.Previous = Head;
	}

	/// <summary>
	/// Removes the <paramref name="node"/> from the list and makes necessary reference corrections.
	/// For private use only.
	/// </summary>
	/// <param name="node">The <see cref="Node"/></param>
	private void InternalRemove(Node node)
	{
		if (node is NullNode) return;

		//Detach the 'node' from the linked list
		if (node.Previous is not NullNode) node.Previous.Next = node.Next;
		if (node.Next is not NullNode) node.Next.Previous = node.Previous!;

		//If required make Head reference corrections
		if (IsHead(node)) Head = node.Next!;
	}

	/// <summary>
	/// Checks if the <paramref name="node"/> is Head of the linked list.
	/// </summary>
	/// <param name="node">The node.</param>
	/// <returns>True if the node is Head node, false otherwise.</returns>
	private Boolean IsHead([NotNull] Node node)
	{
		ArgumentNullException.ThrowIfNull(node, nameof(node));
		return Head == node;
	}

	/// <summary>
	/// Checks if the <paramref name="node"/> is Tail of the linked list.
	/// </summary>
	/// <param name="node">The node.</param>
	/// <returns>True if the node is Tail node, false otherwise.</returns>
	private Boolean IsTail([NotNull] Node node)
	{
		ArgumentNullException.ThrowIfNull(node, nameof(node));
		return Tail == node;
	}
	#endregion Private Methods
}
