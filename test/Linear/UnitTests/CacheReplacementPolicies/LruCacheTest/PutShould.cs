namespace Dsa.Linear.UnitTests.CacheReplacementPolicies.LruCacheTest;
public class PutShould
{
	[Fact]
	public void NotThrow_NotImplementedException()
	{
		//Arrange
		var cache = new LruCache(2);

		//Act
		var act = () => cache.Put(1, 1);

		//Assert
		act.Should().NotThrow<NotImplementedException>();
	}

	[Fact]
	public void AddKeyValuePair_WhenKeyDoesNotExistsAndCacheIsNotFull()
	{
		//Arrange
		var cache = new LruCache(2);

		//Act
		cache.Put(1, 1);
		cache.Put(2, 2);

		var actualValueWhenKeyIs1 = cache.Get(1);
		var actualValueWhenKeyIs2 = cache.Get(2);

		//Assert
		actualValueWhenKeyIs1.Should().Be(1);
		actualValueWhenKeyIs2.Should().Be(2);
	}

	[Fact]
	public void UpdateValue_WhenKeyExists()
	{
		//Arrange
		var cache = new LruCache(2);

		//Act
		cache.Put(1, 1);
		cache.Put(2, 2);
		cache.Put(2, 200);
		cache.Put(1, 100);

		var actualValueWhenKeyIs1 = cache.Get(1);
		var actualValueWhenKeyIs2 = cache.Get(2);

		//Assert
		actualValueWhenKeyIs1.Should().Be(100);
		actualValueWhenKeyIs2.Should().Be(200);
	}

	[Fact]
	public void ReplaceLeastRecentlyUsedValue_WhenCacheIsFull()
	{
		//Arrange
		var cache = new LruCache(2);

		//Act
		cache.Put(1, 1);
		cache.Put(2, 2);
		cache.Get(1);
		cache.Put(3, 3);
		cache.Get(1);
		cache.Put(4, 4);

		var actualValueWhenKeyIs1 = cache.Get(1);
		var actualValueWhenKeyIs2 = cache.Get(2);
		var actualValueWhenKeyIs3 = cache.Get(3);
		var actualValueWhenKeyIs4 = cache.Get(4);

		//Assert
		actualValueWhenKeyIs1.Should().Be(1);
		actualValueWhenKeyIs2.Should().Be(-1);
		actualValueWhenKeyIs3.Should().Be(-1);
		actualValueWhenKeyIs4.Should().Be(4);
	}
}
