namespace Dsa.Linear.UnitTests.LRUCache;

using Dsa.Linear.LRUCache;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;
using static CommonSetup;

public class SetShould
{
	[Fact]
	public void NotThrow_NotImplementedException()
	{
		//Arrange
		var lruCache = new LruCache(4);

		//Act
		var act = () => lruCache.Set(1, 11);

		//Assert
		act.Should().NotThrow<NotImplementedException>();
	}

	[Fact]
	public void AddKeyValuePair_WhenKeyDoesNotExistsAndCacheIsNotFull()
	{
		//Arrange
		var lruCache = new LruCache(4);

		//Act
		lruCache.Set(1, 11);
		var actualValue = lruCache.Get(1);

		//Assert
		actualValue.Should().Be(11);
	}

	[Fact]
	public void UpdateKeyValuePair_WhenKeyExistsInTheCache()
	{
		//Arrange
		var lruCache = new LruCache(4);
		lruCache.Set(1, 11);

		var valueBeforeUpdatingKVP = lruCache.Get(1);

		//Act
		lruCache.Set(1, Int32.MaxValue);
		var valueAfterUpdatingKVP = lruCache.Get(1);

		//Assert
		valueBeforeUpdatingKVP.Should().Be(11);
		valueAfterUpdatingKVP.Should().Be(Int32.MaxValue);
	}

	[Theory]
	[InlineData(1, "10", "1000", "1", "111", "-1,111")]
	[InlineData(2, "10,20", "1000,2000", "1,2", "111,222", "-1,-1,111,222")]
	[InlineData(3, "10,20,30", "1000,2000,3000", "10,30,1", "1001,3003,111", "1001,-1,3003,111")]
	[InlineData(4, "10,20", "1000,2000", "1,2,3,4", "11,22,33,44", "-1,-1,11,22,33,44")]
	public void ReplaceLeastRecentlyUsedKeyValuePair_WhenKeyDoesNotExistsAndCacheIsFull(
		Int32 capacity,
		String keysForArrange,
		String valuesForArrange,
		String keysForAct,
		String valuesForAct,
		String strExpectedValues)
	{
		//Arrange
		var expectedValues = GetInt32Array(strExpectedValues);
		var arrangeKeys = GetInt32Array(keysForArrange);
		var arrangeValues = GetInt32Array(valuesForArrange);
		var cache = new LruCache(capacity);
		for (var i = 0; i < arrangeKeys.Length; i++) cache.Set(arrangeKeys[i], arrangeValues[i]);

		//Act
		var actKeys = GetInt32Array(keysForAct);
		var actValues = GetInt32Array(valuesForAct);
		for (var i = 0; i < actKeys.Length; i++) cache.Set(actKeys[i], actValues[i]);

		var keysMayHaveDuplicates = new Int32[arrangeKeys.Length + actKeys.Length];
		Array.Copy(arrangeKeys, 0, keysMayHaveDuplicates, 0, arrangeKeys.Length);
		Array.Copy(actKeys, 0, keysMayHaveDuplicates, arrangeKeys.Length, actValues.Length);

		var keys = keysMayHaveDuplicates.Distinct().ToArray();
		var values = new Int32[keys.Length];
		for (var i = 0; i < keys.Length; i++) values[i] = cache.Get(keys[i]);

		//Assert
		values.Should().NotBeNullOrEmpty()
			.And.HaveCount(expectedValues.Length)
			.And.BeEquivalentTo(expectedValues);
	}
}
