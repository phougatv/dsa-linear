namespace Dsa.Linear.UnitTests.Generics.LinkedList.DoublyCircularTests;
public class AddHeadShould
{
	[Fact]
	public void NotThrow_NotImplementedException()
	{
		//Arrange
		var linkedList = new DoublyCircularLinkedList<Int32>();

		//Act
		var act = () => linkedList.AddHead(1);

		//Assert
		act.Should().NotThrow<NotImplementedException>();
	}

	[Theory]
	[InlineData("")]
	[InlineData(null)]
	[InlineData("Bruce Wayne")]
	[InlineData("Clark Kent")]
	public void Successfully_AddsNodeToTheHead_WhenListTypeIsOfString_AndListIsEmpty(String keyToBeAdded)
	{
		//Arrange
		var linkedList = new DoublyCircularLinkedList<String>();

		//Act
		linkedList.AddHead(keyToBeAdded);

		//Assert
		linkedList.IsEmpty.Should().BeFalse();
		linkedList.Count.Should().Be(1);
		linkedList.Head.Should().NotBeNull();
		linkedList.Head.Key.Should().Be(keyToBeAdded);
		linkedList.Tail.Should().NotBeNull();
		linkedList.Tail.Key.Should().Be(keyToBeAdded);
	}

	[Theory]
	[InlineData(Int32.MinValue)]
	[InlineData(Int32.MaxValue)]
	[InlineData(-10)]
	[InlineData(10)]
	[InlineData(0)]
	public void Successfully_AddsNodeToTheHead_WhenListTypeIsOfInt32_AndListIsEmpty(Int32 keyToBeAdded)
	{
		//Arrange
		var linkedList = new DoublyCircularLinkedList<Int32>();

		//Act
		linkedList.AddHead(keyToBeAdded);

		//Assert
		linkedList.IsEmpty.Should().BeFalse();
		linkedList.Count.Should().Be(1);
		linkedList.Head.Should().NotBeNull().And.Be(linkedList.Tail);
		linkedList.Head.Key.Should().Be(keyToBeAdded);
		linkedList.Tail.Key.Should().Be(keyToBeAdded);
	}

	[Theory]
	[InlineData("Bruce Wayne,Clark Kent", "", 3, false, "", "Clark Kent")]
	[InlineData("Bruce Wayne", null, 2, false, null, "Bruce Wayne")]
	[InlineData("", "", 2, false, "", "")]
	[InlineData("", null, 2, false, null, "")]
	public void Successfully_AddsNodeToTheHead_WhenListTypeIsOfString_AndListHasOneOrMoreItems(
		String csStrings,
		String keyToBeAdded,
		Int32 expectedCount,
		Boolean expectedIsEmpty,
		String expectedHead,
		String expectedTail)
	{
		//Arrange
		var strArray = csStrings.Split(',');
		var linkedList = new DoublyCircularLinkedList<String>(strArray);

		//Act
		linkedList.AddHead(keyToBeAdded);

		//Assert
		linkedList.IsEmpty.Should().Be(expectedIsEmpty);
		linkedList.Count.Should().Be(expectedCount);
		linkedList.Head.Should().NotBeNull();
		linkedList.Head.Key.Should().Be(expectedHead);
		linkedList.Tail.Should().NotBeNull();
		linkedList.Tail.Key.Should().Be(expectedTail);
	}

	[Theory]
	[InlineData("-99,-1,0,1,99", Int32.MinValue, 6, false, Int32.MinValue, 99)]
	[InlineData("-99,-1,0,1,99", Int32.MaxValue, 6, false, Int32.MaxValue, 99)]
	[InlineData("0", 0, 2, false, 0, 0)]
	[InlineData("34,56", 78, 3, false, 78, 56)]
	public void Successfully_AddsNodeToTheHead_WhenListTypeIsOfInt32_AndListHasOneOrMoreItems(
		String csStrings,
		Int32 keyToBeAdded,
		Int32 expectedCount,
		Boolean expectedIsEmpty,
		Int32 expectedHead,
		Int32 expectedTail)
	{
		//Arrange
		var i32Array = GetInt32Array(csStrings);
		var linkedList = new DoublyCircularLinkedList<Int32>(i32Array);

		//Act
		linkedList.AddHead(keyToBeAdded);

		//Assert
		linkedList.IsEmpty.Should().Be(expectedIsEmpty);
		linkedList.Count.Should().Be(expectedCount);
		linkedList.Head.Should().NotBeNull();
		linkedList.Head.Key.Should().Be(expectedHead);
		linkedList.Tail.Should().NotBeNull();
		linkedList.Tail.Key.Should().Be(expectedTail);
	}
}
