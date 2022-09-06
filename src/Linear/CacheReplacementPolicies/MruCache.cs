namespace Dsa.Linear.CacheReplacementPolicies;
/// <summary>
/// Mru cache class.
/// For details, visit - https://en.wikipedia.org/wiki/Cache_replacement_policies#Most_recently_used_(MRU)
/// </summary>
public sealed class MruCache : CacheReplacementPolicy
{
	public MruCache(Int32 capacity)
		: base(capacity)
	{ }

	public override Int32 Get(Int32 key)
	{
		if (!_keyNodeMap.TryGetValue(key, out var cacheNode)) return -1;
		ReorderCache(key, cacheNode);
		return cacheNode.Value;
	}

	public override void Put(Int32 key, Int32 value)
	{
		if (_keyNodeMap.TryGetValue(key, out var cacheNode))
		{
			ReorderCache(key, cacheNode);
			cacheNode.Value = value;
			return;
		}

		cacheNode = new CacheNode(key, value);
		if (IsFull)
		{
			Remove(Head);
			Add(key, cacheNode);
			return;
		}

		Add(key, cacheNode);
	}
}
