namespace Dsa.Linear.UnitTests.LinkedList.DoublyCircular;

using Dsa.Linear.LinkedList.Doubly;

public class AddTailShould
{
	[Fact]
	public void NotThrow_NotImplementedException()
	{
		//Arrange
		var linkedList = new DoublyCircularLinkedList();

		//Act
		var act = () => linkedList.AddTail(1);

		//Assert
		act.Should().NotThrow<NotImplementedException>();
	}

	[Theory]
	[InlineData(Int32.MinValue, 1)]
	[InlineData(Int32.MaxValue, 1)]
	[InlineData(99, 1)]
	[InlineData(-99, 1)]
	public void Successfully_AddsNode_WhenListIsEmpty(
		Int32 expectedKeyToBeAdded,
		Int32 expectedCount)
	{
		//Arrange
		var linkedList = new DoublyCircularLinkedList();

		//Act
		linkedList.AddTail(expectedKeyToBeAdded);

		//Assert
		linkedList.IsEmpty.Should().BeFalse();
		linkedList.Count.Should().Be(expectedCount);
		linkedList.Head.Should().NotBeNull().And.Be(linkedList.Tail);
		linkedList.Head.Key.Should().Be(expectedKeyToBeAdded);
		linkedList.Tail.Key.Should().Be(expectedKeyToBeAdded);
	}

	[Theory]
	[InlineData(false, "1", 0, 2)]
	[InlineData(false, "40", -190, 2)]
	[InlineData(false, "1,0", 0, 3)]
	[InlineData(false, "99,10,40", 9, 4)]
	[InlineData(false, "40,150,111,43", -190, 5)]
	public void Successfully_AddsNode_WhenListHasOneOrMoreItems(
		Boolean expectedIsEmpty,
		String csInt32s,
		Int32 expectedKey,
		Int32 expectedCount)
	{
		//Arrange
		var i32Array = GetInt32Array(csInt32s);
		var linkedList = new DoublyCircularLinkedList(i32Array);

		//Act
		linkedList.AddTail(expectedKey);

		//Assert
		linkedList.IsEmpty.Should().Be(expectedIsEmpty);
		linkedList.Count.Should().Be(expectedCount);
		linkedList.Head.Should().NotBeNull();
		linkedList.Head.Key.Should().Be(i32Array[0]);
		linkedList.Tail.Should().NotBeNull();
		linkedList.Tail.Key.Should().Be(expectedKey);
	}
}
