namespace Dsa.Linear.UnitTests.LRUCache;

using Dsa.Linear.LRUCache;
using FluentAssertions;
using System;
using Xunit;

public class GetShould
{
	[Fact]
	public void NotThrow_NotImplementedException()
	{
		//Arrange
		var lruCache = new LruCache(4);
		lruCache.Set(1, 11);

		//Act
		var act = () => lruCache.Get(1);

		//Assert
		act.Should().NotThrow<NotImplementedException>();
	}

	[Fact]
	public void ReturnMinusOne_WhenTheCacheIsEmpty()
	{
		//Arrange
		var lruCache = new LruCache(4);

		//Act
		var actualValue = lruCache.Get(1);

		//Assert
		actualValue.Should().Be(-1);
	}

	[Fact]
	public void ReturnMinusOne_WhenKeyDoesNotExistsInTheCache()
	{
		//Arrange
		var lruCache = new LruCache(4);
		lruCache.Set(1, 11);

		//Act
		var actualValue = lruCache.Get(2);

		//Assert
		actualValue.Should().Be(-1);
	}

	[Fact]
	public void ReturnValue_WhenKeyExistsInTheCache()
	{
		//Arrange
		var lruCache = new LruCache(4);
		lruCache.Set(1, 11);
		lruCache.Set(2, 22);
		lruCache.Set(3, 33);
		lruCache.Set(4, 44);

		//Act
		var actualValue1 = lruCache.Get(1);
		var actualValue2 = lruCache.Get(2);
		var actualValue3 = lruCache.Get(3);
		var actualValue4 = lruCache.Get(4);

		//Assert
		actualValue1.Should().Be(11);
		actualValue2.Should().Be(22);
		actualValue3.Should().Be(33);
		actualValue4.Should().Be(44);
	}
}
