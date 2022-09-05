namespace Dsa.Linear.UnitTests.CacheReplacementPolicies.LruCacheTests;
public class GetShould
{
	[Fact]
	public void NotThrow_NotImplementedException()
	{
		//Arrange
		var cache = new LruCache(2);

		//Act
		var act = () => cache.Get(1);

		//Assert
		act.Should().NotThrow<NotImplementedException>();
	}

	[Fact]
	public void ReturnMinusOne_WhenElementWithTheSpecifiedKeyDoesNotExists()
	{
		//Arrange
		var cache = new LruCache(2);
		cache.Put(1, 1);
		cache.Put(2, 2);
		cache.Get(1);
		cache.Put(4, 4);
		cache.Get(1);
		cache.Put(5, 5);

		//Act
		var actualValueWhenKeyIs2 = cache.Get(2);
		var actualValueWhenKeyIs3 = cache.Get(3);
		var actualValueWhenKeyIs4 = cache.Get(4);

		//Assert
		actualValueWhenKeyIs2.Should().Be(-1);
		actualValueWhenKeyIs3.Should().Be(-1);
		actualValueWhenKeyIs4.Should().Be(-1);
	}

	[Fact]
	public void ReturnValue_WhenElementWithTheSpecifiedKeyExists()
	{
		//Arrange
		var cache = new LruCache(2);
		cache.Put(1, 1);
		cache.Put(2, 2);
		cache.Get(1);
		cache.Put(4, 4);
		cache.Get(1);
		cache.Put(5, 5);

		//Act
		var actualValueWhenKeyIs1 = cache.Get(1);
		var actualValueWhenKeyIs5 = cache.Get(5);

		//Assert
		actualValueWhenKeyIs1.Should().Be(1);
		actualValueWhenKeyIs5.Should().Be(5);
	}
}
