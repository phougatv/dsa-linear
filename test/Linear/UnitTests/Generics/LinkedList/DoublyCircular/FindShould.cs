namespace Dsa.Linear.UnitTests.Generics.LinkedList.DoublyCircular;

public class FindShould
{
	[Fact]
	public void NotThrow_NotImplementedException()
	{
		//Arrange
		var linkedList = new DoublyCircularLinkedList<Int32>();

		//Act
		var act = () => linkedList.Find(1);

		//Assert
		act.Should().NotThrow<NotImplementedException>();
	}

	[Theory]
	[InlineData(Int32.MinValue)]
	[InlineData(Int32.MaxValue)]
	[InlineData(0)]
	public void ReturnsNull_WhenListTypeIsInt32_AndItIsEmpty(Int32 keyToBeSearched)
	{
		//Arrange
		var linkedList = new DoublyCircularLinkedList<Int32>();

		//Act
		var actualNode = linkedList.Find(keyToBeSearched);

		//Assert
		actualNode.Should().BeNull();
		linkedList.Count.Should().Be(0);
		linkedList.IsEmpty.Should().BeTrue();
		linkedList.Head.Should().BeNull().And.Be(linkedList.Tail);
	}

	[Theory]
	[InlineData("")]
	[InlineData("ab")]
	[InlineData(null)]
	public void ReturnsNull_WhenListTypeIsString_AndItIsEmpty(
		String keyToBeSearched)
	{
		//Arrange
		var linkedList = new DoublyCircularLinkedList<String>();

		//Act
		var actualNode = linkedList.Find(keyToBeSearched);

		//Assert
		actualNode.Should().BeNull();
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
	public void ReturnsNull_WhenListTypeIsInt32_AndTheItemDoesNotExistsInIt(
		String csInts,
		Int32 keyToBeSearched)
	{
		//Arrange
		var i32Array = GetInt32Array(csInts);
		var linkedList = new DoublyCircularLinkedList<Int32>(i32Array);

		//Act
		var actualNode = linkedList.Find(keyToBeSearched);

		//Assert
		actualNode.Should().BeNull();
		linkedList.IsEmpty.Should().BeFalse();
		linkedList.Count.Should().Be(i32Array.Length);
		linkedList.Head.Should().NotBeNull();
		linkedList.Head.Key.Should().Be(i32Array[0]);
		linkedList.Tail.Should().NotBeNull();
		linkedList.Tail.Key.Should().Be(i32Array[^1]);
	}

	[Theory]
	[InlineData("Bruce,Clark", "", "Tony", 3, "Bruce", "")]
	[InlineData("Bruce,Clark,Tony", null, "", 4, "Bruce", null)]
	[InlineData("Bruce,Clark,Tony", "Steve", null, 4, "Bruce", "Steve")]
	public void ReturnsNull_WhenListTypeIsString_AndTheItemDoesNotExistsInIt(
		String csStrings,
		String tailKey,
		String keyToBeSearched,
		Int32 expectedCount,
		String expectedHeadKey,
		String expectedTailKey)
	{
		//Arrange
		var linkedList = new DoublyCircularLinkedList<String>(csStrings.Split(','));
		linkedList.Add(tailKey);

		//Act
		var actualNode = linkedList.Find(keyToBeSearched);

		//Assert
		actualNode.Should().BeNull();
		linkedList.IsEmpty.Should().BeFalse();
		linkedList.Count.Should().Be(expectedCount);
		linkedList.Head.Should().NotBeNull();
		linkedList.Head.Key.Should().Be(expectedHeadKey);
		linkedList.Tail.Should().NotBeNull();
		linkedList.Tail.Key.Should().Be(expectedTailKey);
	}

	[Theory]
	[InlineData(33)]
	[InlineData(19)]
	[InlineData(8)]
	[InlineData(1)]
	[InlineData(0)]
	[InlineData(-9900)]
	public void ReturnsNode_WhenTypeIsInt32_AndTheItemExistsInTheList(Int32 keyToBeSearched)
	{
		//Arrange
		var i32Array = new Int32[] { -9900, 0, 1, 8, 19, 33 };
		var linkedList = new DoublyCircularLinkedList<Int32>(i32Array);

		//Act
		var actualNode = linkedList.Find(keyToBeSearched);

		//Assert
		actualNode.Should().NotBeNull().And.BeOfType<Node<Int32>>();
		actualNode.Key.Should().Be(keyToBeSearched);
		linkedList.IsEmpty.Should().BeFalse();
		linkedList.Count.Should().Be(i32Array.Length);
		linkedList.Head.Should().NotBeNull();
		linkedList.Head.Key.Should().Be(i32Array[0]);
		linkedList.Tail.Should().NotBeNull();
		linkedList.Tail.Key.Should().Be(i32Array[^1]);
	}

	[Theory]
	[InlineData("")]
	[InlineData(null)]
	[InlineData("Bruce Wayne")]
	[InlineData("Clark Kent")]
	[InlineData("Tony Stark")]
	public void ReturnsNode_WhenTypeIsString_AndTheItemExistsInTheList(
		String keyToBeSearched)
	{
		//Arrange
		var strArray = new String[] { "Bruce Wayne", "Clark Kent", "Tony Stark", String.Empty, null! };
		var linkedList = new DoublyCircularLinkedList<String>(strArray);

		//Act
		var actualNode = linkedList.Find(keyToBeSearched);

		//Assert
		actualNode.Should().NotBeNull().And.BeOfType<Node<String>>();
		actualNode.Key.Should().Be(keyToBeSearched);
		linkedList.IsEmpty.Should().BeFalse();
		linkedList.Count.Should().Be(strArray.Length);
		linkedList.Head.Should().NotBeNull();
		linkedList.Head.Key.Should().Be(strArray[0]);
		linkedList.Tail.Should().NotBeNull();
		linkedList.Tail.Key.Should().Be(strArray[^1]);
	}

	[Fact]
	public void ReturnsNode_WhenTypeIsString_ItemToBeSearchIsStringEmptyAndTheListContainsNull()
	{
		//Arrange
		var keyToBeSearched = String.Empty;
		var strArray = new String[] { "Bruce Wayne", "Clark Kent", "Tony Stark", String.Empty, null! };
		var linkedList = new DoublyCircularLinkedList<String>(strArray);

		//Act
		var actualNode = linkedList.Find(keyToBeSearched);

		//Assert
		actualNode.Should().NotBeNull().And.BeOfType<Node<String>>();
		actualNode.Key.Should().Be(keyToBeSearched);
		linkedList.IsEmpty.Should().BeFalse();
		linkedList.Count.Should().Be(strArray.Length);
		linkedList.Head.Should().NotBeNull();
		linkedList.Head.Key.Should().Be(strArray[0]);
		linkedList.Tail.Should().NotBeNull();
		linkedList.Tail.Key.Should().Be(strArray[^1]);
	}
}
