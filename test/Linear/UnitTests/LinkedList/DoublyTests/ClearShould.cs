namespace Dsa.Linear.UnitTests.LinkedList.DoublyTests;
public class ClearShould
{
	[Fact]
	public void NotThrow_NotImplementedException()
	{
		//Arrange
		var linkedList = new DoublyLinkedList();

		//Act
		var act = () => linkedList.Clear();

		//Assert
		act.Should().NotThrow<NotImplementedException>();
	}

	[Fact]
	public void Successfully_Clears_EvenWhenTheListIsEmpty()
	{
		//Arrange
		var linkedList = new DoublyLinkedList();

		//Act
		linkedList.Clear();

		//Assert
		linkedList.Count.Should().Be(0);
		linkedList.IsEmpty.Should().BeTrue();
		linkedList.Head.Should().BeNull().And.Be(linkedList.Tail);
	}

	[Theory]
	[InlineData("1", 0)]
	[InlineData("1,0", 0)]
	[InlineData("99,10,40", 0)]
	[InlineData("40,150,111,43", 0)]
	[InlineData("40,150,111,43,22", 0)]
	public void Successfully_Clears_WhenTheListHasTwoOrMoreItems(
		String csInt32s,
		Int32 expectedCount)
	{
		//Arrange
		var i32Array = GetInt32Array(csInt32s);
		var linkedList = new DoublyLinkedList(i32Array);

		//Act
		linkedList.Clear();

		//Assert
		linkedList.IsEmpty.Should().BeTrue();
		linkedList.Count.Should().Be(expectedCount);
		linkedList.Head.Should().BeNull().And.Be(linkedList.Tail);
	}
}
