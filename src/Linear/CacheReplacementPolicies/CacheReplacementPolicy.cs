namespace Dsa.Linear.CacheReplacementPolicies;
/// <summary>
/// Cache replacement policy class
/// For details visit - https://en.wikipedia.org/wiki/Cache_replacement_policies
/// </summary>
public abstract class CacheReplacementPolicy
{
	#region Internal Class - CacheNode
	public class CacheNode : IEquatable<CacheNode>
	{
		#region Internal Properties
		public Int32 Key { get; internal set; }
		public Int32 Value { get; internal set; }
		public CacheNode Next { get; internal set; }
		public CacheNode Prev { get; internal set; }
		#endregion Internal Properties

		#region Internal Ctors 
		internal CacheNode(Int32 key, Int32 value)
		{
			Key = key;
			Value = value;
			Next = Prev = null!;
		}
		#endregion Internal Ctors

		#region Overloaded Operators
		public static Boolean operator ==(CacheNode left, CacheNode right)
		{
			if (ReferenceEquals(left, right)) return true;
			if (left is null) return false;
			if (right is null) return false;
			return left.Key == right.Key && left.Value == right.Value;
		}
		public static Boolean operator !=(CacheNode left, CacheNode right)
			=> !(left == right);
		public static Boolean operator >(CacheNode left, CacheNode right)
		{
			if (ReferenceEquals(left, right)) return false;
			if (left is null) return false;
			if (right is null) return false;

			return left.Key == right.Key && left.Value > right.Value;
		}
		public static Boolean operator <(CacheNode left, CacheNode right)
		{
			if (ReferenceEquals(left, right)) return false;
			if (left is null) return false;
			if (right is null) return false;

			return left.Key == right.Key && left.Value < right.Value;
		}
		public override Boolean Equals(Object? obj)
			=> obj is CacheNode cacheNode && Equals(cacheNode);
		public Boolean Equals(CacheNode? cacheNode) => this == cacheNode!;
		#endregion Overloaded Operators
	}
	#endregion Internal Class - CacheNode

	#region Class Variables
	protected readonly Dictionary<Int32, CacheNode> _keyNodeMap;
	protected Int32 _capacity;
	#endregion Protected Members

	#region Properties
	//Protected
	protected Int32 Count { get; private set; }
	protected Boolean HasCapacity => Count < _capacity;
	protected CacheNode Head { get; private set; }
	protected Boolean IsEmpty => Count == 0 && Head is null;
	protected Boolean IsFull => Count == _capacity;
	protected CacheNode Tail => Head?.Prev!;
	#endregion Properties

	#region Ctor
	protected CacheReplacementPolicy(Int32 capacity)
	{
		if (capacity < 0) throw new ArgumentOutOfRangeException(nameof(capacity));
		Count = 0;
		_capacity = capacity;
		_keyNodeMap = new Dictionary<Int32, CacheNode>(capacity);
		Head = null!;
	}
	#endregion Ctor

	#region Methods
	//---- PUBLIC ABSTRACT ----

	public abstract Int32 Get(Int32 key);
	public abstract void Put(Int32 key, Int32 value);

	//---- PROTECTED ----

	/// <summary>
	/// Adds <paramref name="cacheNode"/> to the the cache-node list <paramref name="key"/> and <paramref name="cacheNode"/> to the the cache-node list
	/// </summary>
	/// <param name="key"></param>
	/// <param name="cacheNode"></param>
	protected void Add(Int32 key, CacheNode cacheNode)
	{
		AddCacheNode(cacheNode);
		_keyNodeMap.Add(key, cacheNode);
		IncreaseCountBy1();
	}
	protected void Remove(CacheNode cacheNode)
	{
		RemoveCacheNode(cacheNode);
		if (_keyNodeMap.Remove(cacheNode.Key))
			DecreaseCountBy1();
	}

	/// <summary>
	/// Reorders the cache-node list by first removing and then adding the <paramref name="key"/> and <paramref name="cacheNode"/> to the list.
	/// </summary>
	/// <param name="key">The key</param>
	/// <param name="cacheNode">The cache node</param>
	protected void ReorderCache(Int32 key, CacheNode cacheNode)
	{
		Remove(cacheNode);
		Add(key, cacheNode);
	}

	//---- PRIVATE ----

	/// <summary>
	/// Adds the <paramref name="cacheNode"/> to the start of the cache-node list
	/// </summary>
	/// <param name="cacheNode">The cache node</param>
	private void AddCacheNode(CacheNode cacheNode)
	{
		if (cacheNode is null) return;
		if (Head is null)
		{
			Head = cacheNode;
			Head.Next = Head;
			Head.Prev = Head;
			return;
		}

		cacheNode.Prev = Tail;
		cacheNode.Next = Head;
		Head.Prev = Tail.Next = cacheNode;
		Head = cacheNode;
	}

	/// <summary>
	/// Decreased the count by 1
	/// </summary>
	private void DecreaseCountBy1() => Count = Count - 1;

	/// <summary>
	/// Increases the count by 1
	/// </summary>
	private void IncreaseCountBy1() => Count = Count + 1;

	/// <summary>
	/// Removes the <paramref name="cacheNode"/> from the cache-node list
	/// </summary>
	/// <param name="cacheNode">The cache node</param>
	private void RemoveCacheNode(CacheNode cacheNode)
	{
		if (cacheNode is null) return;
		if (IsEmpty) return;
		if (Count == 1)
		{
			Head = cacheNode.Next = cacheNode.Prev = null!;
			return;
		}

		cacheNode.Next.Prev = cacheNode.Prev;
		cacheNode.Prev.Next = cacheNode.Next;
		if (cacheNode == Head) Head = cacheNode.Next;
		cacheNode.Prev = cacheNode.Next = null!;
	}
	#endregion Methods
}
