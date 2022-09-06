namespace Dsa.Linear.UnitTests.Generics.LinkedList.DoublyCircularTest;
public class RemoveShould
{
	[Fact]
	public void NotThrow_NotImplementedException()
	{
		//Arrange
		var linkedList = new DoublyCircularLinkedList<Int32>();

		//Act
		var act = () => linkedList.Remove(1);

		//Assert
		act.Should().NotThrow<NotImplementedException>();
	}

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData("Bruce")]
	[InlineData("Wayne")]
	public void Successfully_Executes_WhenTheListIsOfTypeString_AndIsEmpty(String keyToBeRemoved)
	{
		//Arrange
		var linkedList = new DoublyCircularLinkedList<String>();

		//Act
		linkedList.Remove(keyToBeRemoved);

		//Assert
		linkedList.IsEmpty.Should().BeTrue();
		linkedList.Count.Should().Be(0);
		linkedList.Head.Should().BeNull();
		linkedList.Tail.Should().BeNull();
	}

	[Theory]
	[InlineData(Int32.MaxValue)]
	[InlineData(Int32.MinValue)]
	[InlineData(0)]
	[InlineData(-99)]
	public void Successfully_Executes_WhenTheListIsOfTypeInt32_AndIsEmpty(Int32 keyToBeRemoved)
	{
		//Arrange
		var linkedList = new DoublyCircularLinkedList<Int32>();

		//Act
		linkedList.Remove(keyToBeRemoved);

		//Assert
		linkedList.IsEmpty.Should().BeTrue();
		linkedList.Count.Should().Be(0);
		linkedList.Head.Should().BeNull();
		linkedList.Tail.Should().BeNull();
	}

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData("Bruce")]
	[InlineData("Wayne")]
	public void Successfully_RemovesTheNodeWithTheKey_WhenTheListIsOfTypeString_AndHasOnlyOneItem(String key)
	{
		//Arrange
		var linkedList = new DoublyCircularLinkedList<String>(new String[] { key });

		//Act
		linkedList.Remove(key);

		//Assert
		linkedList.IsEmpty.Should().BeTrue();
		linkedList.Count.Should().Be(0);
		linkedList.Head.Should().BeNull();
		linkedList.Tail.Should().BeNull();
	}

	[Theory]
	[InlineData(Int32.MaxValue)]
	[InlineData(Int32.MinValue)]
	[InlineData(0)]
	[InlineData(-99)]
	public void Successfully_RemovesTheNodeWithTheKey_WhenTheListIsOfTypeInt32_AndHasOnlyOneItem(Int32 key)
	{
		//Arrange
		var linkedList = new DoublyCircularLinkedList<Int32>(new Int32[] { key });

		//Act
		linkedList.Remove(key);

		//Assert
		linkedList.IsEmpty.Should().BeTrue();
		linkedList.Count.Should().Be(0);
		linkedList.Head.Should().BeNull();
		linkedList.Tail.Should().BeNull();
	}

	[Theory]
	[InlineData("Bruce,Clark,Steve", "", "", "Bruce", "Steve", 3, false)]
	[InlineData("Bruce,Clark,Steve", null, null, "Bruce", "Steve", 3, false)]
	[InlineData("Bruce,Clark,Steve", "", "Steve", "Bruce", "", 3, false)]
	[InlineData("Bruce,Clark,Steve", null, "Clark", "Bruce", null, 3, false)]
	[InlineData("Bruce,Clark,Steve", "", "Bruce", "Clark", "", 3, false)]
	public void Successfully_RemovesTheNodeWithTheKey_WhenTheListIsOfTypeString_AndHasTwoOrMoreItems(
		String csStrings,
		String tailKey,
		String keyToBeRemoved,
		String expectedHeadKey,
		String expectedTailKey,
		Int32 expectedCount,
		Boolean expectedIsEmpty)
	{
		//Arrange
		var strArray = csStrings.Split(',');
		var linkedList = new DoublyCircularLinkedList<String>(strArray);
		linkedList.Add(tailKey);

		//Act
		linkedList.Remove(keyToBeRemoved);

		//Assert
		linkedList.IsEmpty.Should().Be(expectedIsEmpty);
		linkedList.Count.Should().Be(expectedCount);
		linkedList.Head.Should().NotBeNull();
		linkedList.Head.Key.Should().Be(expectedHeadKey);
		linkedList.Tail.Should().NotBeNull();
		linkedList.Tail.Key.Should().Be(expectedTailKey);
	}

	[Theory]
	[InlineData("-9999", Int32.MinValue, Int32.MinValue, -9999, -9999, 1, false)]
	[InlineData("-9999,-10,-1,0,1,2,3,4", Int32.MinValue, Int32.MinValue, -9999, 4, 8, false)]
	[InlineData("-9999,-10,-1,0,1,2,3,4", Int32.MaxValue, Int32.MaxValue, -9999, 4, 8, false)]
	[InlineData("-9999,-10,-1,0,1,2,3,4", Int32.MinValue, -9999, -10, Int32.MinValue, 8, false)]
	[InlineData("-9999,-10,-1,0,1,2,3,4", Int32.MaxValue, 4, -9999, Int32.MaxValue, 8, false)]
	public void Successfully_RemovesTheNodeWithTheKey_WhenTheListIsOfTypeInt32_AndHasTwoOrMoreItems(
		String csInts,
		Int32 tailKey,
		Int32 keyToBeRemoved,
		Int32 expectedHeadKey,
		Int32 expectedTailKey,
		Int32 expectedCount,
		Boolean expectedIsEmpty)
	{
		//Arrange
		var i32Array = GetInt32Array(csInts);
		var linkedList = new DoublyCircularLinkedList<Int32>(i32Array);
		linkedList.Add(tailKey);

		//Act
		linkedList.Remove(keyToBeRemoved);

		//Assert
		linkedList.IsEmpty.Should().Be(expectedIsEmpty);
		linkedList.Count.Should().Be(expectedCount);
		linkedList.Head.Should().NotBeNull();
		linkedList.Head.Key.Should().Be(expectedHeadKey);
		linkedList.Tail.Should().NotBeNull();
		linkedList.Tail.Key.Should().Be(expectedTailKey);
	}
}
