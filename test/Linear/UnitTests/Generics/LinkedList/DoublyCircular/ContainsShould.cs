namespace Dsa.Linear.UnitTests.Generics.LinkedList.DoublyCircular;
public class ContainsShould
{
	[Fact]
	public void NotThrow_NotImplementedException()
	{
		//Arrange
		var linkedList = new DoublyCircularLinkedList<Int32>();

		//Act
		var act = () => linkedList.Contains(1);

		//Assert
		act.Should().NotThrow<NotImplementedException>();
	}

	[Fact]
	public void ReturnsFalse_WhenTypeIsInt32_AndTheListIsEmpty()
	{
		//Arrange
		var linkedList = new DoublyCircularLinkedList<Int32>();

		//Act
		var actualResult = linkedList.Contains(1);

		//Assert
		actualResult.Should().BeFalse();
		linkedList.Count.Should().Be(0);
		linkedList.IsEmpty.Should().BeTrue();
		linkedList.Head.Should().BeNull().And.Be(linkedList.Tail);
	}

	[Theory]
	[InlineData("")]
	[InlineData("ab")]
	[InlineData(null)]
	public void ReturnsFalse_WhenTypeIsString_AndTheListIsEmpty(
		String keyToBeSearched)
	{
		//Arrange
		var linkedList = new DoublyCircularLinkedList<String>();

		//Act
		var actualResult = linkedList.Contains(keyToBeSearched);

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
	public void ReturnsFalse_WhenTypeIsInt32_AndTheItemDoesNotExistsInTheList(
		String csInts,
		Int32 keyToBeSearched)
	{
		//Arrange
		var i32Array = GetInt32Array(csInts);
		var linkedList = new DoublyCircularLinkedList<Int32>(i32Array);

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
	public void ReturnsTrue_WhenTypeIsInt32_AndTheItemExistsInTheList(
		String csInts,
		Int32 keyToBeSearched)
	{
		//Arrange
		var i32Array = GetInt32Array(csInts);
		var linkedList = new DoublyCircularLinkedList<Int32>(i32Array);

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

	[Theory]
	[InlineData("")]
	[InlineData(null)]
	[InlineData("Bruce Wayne")]
	[InlineData("Clark Kent")]
	[InlineData("Tony Stark")]
	public void ReturnsTrue_WhenTypeIsString_AndTheItemExistsInTheList(
		String keyToBeSearched)
	{
		//Arrange
		var strArray = new String[] { "Bruce Wayne", "Clark Kent", "Tony Stark", String.Empty, null! };
		var linkedList = new DoublyCircularLinkedList<String>(strArray);

		//Act
		var actualResult = linkedList.Contains(keyToBeSearched);

		//Assert
		actualResult.Should().BeTrue();
		linkedList.IsEmpty.Should().BeFalse();
		linkedList.Count.Should().Be(strArray.Length);
		linkedList.Head.Should().NotBeNull();
		linkedList.Head.Key.Should().Be(strArray[0]);
		linkedList.Tail.Should().NotBeNull();
		linkedList.Tail.Key.Should().Be(strArray[^1]);
	}

	[Fact]
	public void ReturnsTrue_WhenTypeIsString_ItemToBeSearchIsStringEmptyAndTheListContainsNull()
	{
		//Arrange
		var keyToBeSearched = String.Empty;
		var strArray = new String[] { "Bruce Wayne", "Clark Kent", "Tony Stark", String.Empty, null! };
		var linkedList = new DoublyCircularLinkedList<String>(strArray);

		//Act
		var actualResult = linkedList.Contains(keyToBeSearched);

		//Assert
		actualResult.Should().BeTrue();
		linkedList.IsEmpty.Should().BeFalse();
		linkedList.Count.Should().Be(strArray.Length);
		linkedList.Head.Should().NotBeNull();
		linkedList.Head.Key.Should().Be(strArray[0]);
		linkedList.Tail.Should().NotBeNull();
		linkedList.Tail.Key.Should().Be(strArray[^1]);
	}
}
