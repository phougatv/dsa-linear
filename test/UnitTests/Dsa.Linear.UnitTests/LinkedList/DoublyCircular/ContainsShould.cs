namespace Dsa.Linear.UnitTests.LinkedList.DoublyCircular;

using Dsa.Linear.LinkedList.Doubly;
using FluentAssertions;
using System;
using Xunit;
using static CommonSetup;

public class ContainsShould
{
	[Fact]
	public void NotThrow_NotImplementedException()
	{
		//Arrange
		var linkedList = new DoublyCircularLinkedList();

		//Act
		var act = () => linkedList.Contains(1);

		//Assert
		act.Should().NotThrow<NotImplementedException>();
	}

	[Fact]
	public void ReturnsFalse_WhenTheListIsEmpty()
	{
		//Arrange
		var linkedList = new DoublyCircularLinkedList();

		//Act
		var actualResult = linkedList.Contains(1);

		//Assert
		actualResult.Should().BeFalse();
		linkedList.Count.Should().Be(0);
		linkedList.IsEmpty.Should().BeTrue();
		linkedList.Head.Should().BeNull().And.Be(linkedList.Tail);
	}

	[Theory]
	[InlineData("-10,-4,-1,0,1,8,9", Int32.MaxValue)]
	[InlineData("-9900,0,1,8,19,33,56,89,88,11,9", Int32.MinValue)]
	[InlineData("-9900,0,1,8,19,33,56,89,88,11,9", 99)]
	[InlineData("-9900,0,1,8,19,33,56,89,88,11,9", 100)]
	[InlineData("-9900,0,1,8,19,33,56,89,88,11,9", 101)]
	public void ReturnsFalse_WhenTheItemDoesNotExistsInTheList(
		String csInts,
		Int32 keyToBeSearched)
	{
		//Arrange
		var i32Array = GetInt32Array(csInts);
		var linkedList = new DoublyCircularLinkedList(i32Array);

		//Act
		var actualResult = linkedList.Contains(keyToBeSearched);

		//Assert
		actualResult.Should().BeFalse();
		linkedList.IsEmpty.Should().BeFalse();
		linkedList.Count.Should().Be(i32Array.Length);
		linkedList.Head.Should().NotBeNull();
		linkedList.Head.Key.Should().Be(i32Array[0]);
		linkedList.Tail.Should().NotBeNull();
		linkedList.Tail.Key.Should().Be(i32Array[^1]);
	}

	[Theory]
	[InlineData("-9900,0,1,8,19,33,56,89,88,11,9", 9)]
	[InlineData("-9900,0,1,8,19,33,56,89,88,11,9", 11)]
	[InlineData("-9900,0,1,8,19,33,56,89,88,11,9", 88)]
	[InlineData("-9900,0,1,8,19,33,56,89,88,11,9", 89)]
	[InlineData("-9900,0,1,8,19,33,56,89,88,11,9", 56)]
	[InlineData("-9900,0,1,8,19,33,56,89,88,11,9", 33)]
	[InlineData("-9900,0,1,8,19,33,56,89,88,11,9", 19)]
	[InlineData("-9900,0,1,8,19,33,56,89,88,11,9", 8)]
	[InlineData("-9900,0,1,8,19,33,56,89,88,11,9", 1)]
	[InlineData("-9900,0,1,8,19,33,56,89,88,11,9", 0)]
	[InlineData("-9900,0,1,8,19,33,56,89,88,11,9", -9900)]
	public void ReturnsTrue_WhenTheItemExistsInTheList(
		String csInts,
		Int32 keyToBeSearched)
	{
		//Arrange
		var i32Array = GetInt32Array(csInts);
		var linkedList = new DoublyCircularLinkedList(i32Array);

		//Act
		var actualResult = linkedList.Contains(keyToBeSearched);

		//Assert
		actualResult.Should().BeTrue();
		linkedList.IsEmpty.Should().BeFalse();
		linkedList.Count.Should().Be(i32Array.Length);
		linkedList.Head.Should().NotBeNull();
		linkedList.Head.Key.Should().Be(i32Array[0]);
		linkedList.Tail.Should().NotBeNull();
		linkedList.Tail.Key.Should().Be(i32Array[^1]);
	}
}
