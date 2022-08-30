namespace Dsa.Linear.UnitTests.LinkedList.DoublyCircular;

using Dsa.Linear.LinkedList.Doubly;

public class RemoveShould
{
	[Fact]
	public void NotThrow_NotImplementedException()
	{
		//Arrange
		var linkedList = new DoublyCircularLinkedList();

		//Act
		var act = () => linkedList.Remove(1);

		//Assert
		act.Should().NotThrow<NotImplementedException>();
	}

	[Fact]
	public void Successfully_Returns_WhenTheListIsEmpty()
	{
		//Arrange
		var linkedList = new DoublyCircularLinkedList();

		//Act
		linkedList.Remove(1);

		//Assert
		linkedList.Count.Should().Be(0);
		linkedList.IsEmpty.Should().BeTrue();
		linkedList.Head.Should().BeNull().And.Be(linkedList.Tail);
	}

	[Theory]
	[InlineData(1, "1,0", 1, 0, 0)]
	[InlineData(10, "99,10,40", 2, 99, 40)]
	[InlineData(40, "40,150,111,43", 3, 150, 43)]
	[InlineData(150, "40,150,111,43", 3, 40, 43)]
	[InlineData(111, "40,150,111,43", 3, 40, 43)]
	[InlineData(43, "40,150,111,43", 3, 40, 111)]
	public void Successfully_RemovesTheHeadKey_WhenTheListHasTwoOrMoreItems(
		Int32 keyToBeRemoved,
		String csInt32s,
		Int32 expectedCount,
		Int32 expectedHeadKey,
		Int32 expectedTailKey)
	{
		//Arrange
		var i32Array = GetInt32Array(csInt32s);
		var linkedList = new DoublyCircularLinkedList(i32Array);

		//Act
		linkedList.Remove(keyToBeRemoved);

		//Assert
		linkedList.IsEmpty.Should().BeFalse();
		linkedList.Count.Should().Be(expectedCount);
		linkedList.Head.Should().NotBeNull();
		linkedList.Head.Key.Should().Be(expectedHeadKey);
		linkedList.Tail.Should().NotBeNull();
		linkedList.Tail.Key.Should().Be(expectedTailKey);
	}
}
