namespace Dsa.Linear.LRUCache;

using VP.DotNet.Assist.Extensions;

/// <summary>
/// LruCache class
/// This class implements Least Recently Used (LRU), a cache replacement algorithm.
/// For more detail see -
///		1. https://en.wikipedia.org/wiki/Cache_replacement_policies
///		2. https://en.wikipedia.org/wiki/Cache_replacement_policies#Least_recently_used_(LRU)
/// </summary>
public class LruCache
{
	#region Private Members
	private readonly Dictionary<Int32, Int32> _keyValueMap;
	private readonly LinkedList<Int32> _keys;
	private readonly Int32 _capacity;
	private Int32 _count;
	#endregion Private Members

	#region Properties
	private Boolean IsNotFull => _count < _capacity;
	public Boolean IsFull => !IsNotFull;
	public Boolean IsEmpty => _count == 0;
	#endregion Properties

	#region Public Ctors
	public LruCache(Int32 capacity)
	{
		if (!capacity.IsPositive())
			throw new ArgumentOutOfRangeException(nameof(capacity));

		_count = 0;
		_capacity = capacity;
		_keys = new LinkedList<Int32>();
		_keyValueMap = new Dictionary<Int32, Int32>(capacity);
	}
	#endregion Public Ctors

	#region Public APIs
	public Int32 Get(Int32 key)
	{
		if (KeyExists(key) && IsKeysReorderSuccess(key))
			return _keyValueMap[key];
		return -1;
	}

	public void Set(Int32 key, Int32 value)
	{
		if (KeyExists(key) && IsKeysReorderSuccess(key))
		{
			_keyValueMap[key] = value;
			return;
		}

		if (IsNotFull)
		{
			AddNewKeyValuePair(key, value);
			return;
		}

		RemoveLeastRecentlyUsedKey();
		AddNewKeyValuePair(key, value);
	}
	#endregion Public APIs

	#region Private Methods
	private void AddNewKeyValuePair(Int32 key, Int32 value)
	{
		_keys.AddFirst(key);
		_keyValueMap.Add(key, value);
		_count++;
	}

	private void RemoveLeastRecentlyUsedKey()
	{
		var lruKey = _keys.Last.Value;
		_keys.RemoveLast();
		_keyValueMap.Remove(lruKey);
		_count--;
	}

	private Boolean IsKeysReorderSuccess(Int32 key)
	{
		var keyNode = _keys.Find(key);
		if (keyNode is null)
			return false;

		_keys.Remove(keyNode);
		_keys.AddFirst(keyNode);
		return true;
	}
	private Boolean KeyExists(Int32 key) => _keyValueMap.ContainsKey(key);
	#endregion Private Methods
}
