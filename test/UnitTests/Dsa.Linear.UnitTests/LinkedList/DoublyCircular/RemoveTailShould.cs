namespace Dsa.Linear.UnitTests.LinkedList.DoublyCircular;

using Dsa.Linear.LinkedList.Doubly;
using FluentAssertions;
using System;
using Xunit;
using static CommonSetup;

public class RemoveTailShould
{
	[Fact]
	public void NotThrow_NotImplementedException()
	{
		//Arrange
		var linkedList = new DoublyCircularLinkedList();

		//Act
		var act = () => linkedList.RemoveTail();

		//Assert
		act.Should().NotThrow<NotImplementedException>();
	}

	[Fact]
	public void Successfully_Executes_EvenWhenTheListIsEmpty()
	{
		//Arrange
		var linkedList = new DoublyCircularLinkedList();

		//Act
		linkedList.RemoveTail();

		//Assert
		linkedList.Count.Should().Be(0);
		linkedList.IsEmpty.Should().BeTrue();
		linkedList.Head.Should().BeNull().And.Be(linkedList.Tail);
	}

	[Theory]
	[InlineData("1,0", 1)]
	[InlineData("99,10,40", 2)]
	[InlineData("40,150,111,43", 3)]
	public void Successfully_RemovesTheHeadKey_WhenTheListHasTwoOrMoreItems(
		String csInt32s,
		Int32 expectedCount)
	{
		//Arrange
		var i32Array = GetInt32Array(csInt32s);
		var linkedList = new DoublyCircularLinkedList(i32Array);

		//Act
		linkedList.RemoveTail();

		//Assert
		linkedList.IsEmpty.Should().BeFalse();
		linkedList.Count.Should().Be(expectedCount);
		linkedList.Head.Should().NotBeNull();
		linkedList.Head.Key.Should().Be(i32Array[0]);
		linkedList.Tail.Should().NotBeNull();
		linkedList.Tail.Key.Should().Be(i32Array[^2]);
	}
}
