namespace Dsa.Linear.UnitTests.Generics.LinkedList.DoublyCircularTests;
public class ClearShould
{
	[Fact]
	public void NotThrow_NotImplementedException()
	{
		//Arrange
		var linkedList = new DoublyCircularLinkedList<String>();

		//Act
		var act = () => linkedList.Clear();

		//Assert
		act.Should().NotThrow<NotImplementedException>();
	}

	[Fact]
	public void Successfully_Executes_WhenTheListIsOfTypeString_AndIsEmpty()
	{
		//Arrange
		var linkedList = new DoublyCircularLinkedList<String>();

		//Act
		linkedList.Clear();

		//Assert
		linkedList.Count.Should().Be(0);
		linkedList.IsEmpty.Should().BeTrue();
		linkedList.Head.Should().BeNull().And.Be(linkedList.Tail);
	}

	[Fact]
	public void Successfully_Executes_WhenTheListIsOfTypeint32_AndIsEmpty()
	{
		//Arrange
		var linkedList = new DoublyCircularLinkedList<Int32>();

		//Act
		linkedList.Clear();

		//Assert
		linkedList.Count.Should().Be(0);
		linkedList.IsEmpty.Should().BeTrue();
		linkedList.Head.Should().BeNull().And.Be(linkedList.Tail);
	}

	[Theory]
	[InlineData("Bruce Wayne, Batman,Clark Kent,Superman")]
	[InlineData("Bruce Wayne, Batman,Clark Kent")]
	[InlineData("Bruce Wayne, Batman")]
	[InlineData("Bruce Wayne")]
	public void Successfully_ClearsTheListOfTypeString_WhenItOneOrMoreElements(String csStrings)
	{
		//Arrange
		var linkedList = new DoublyCircularLinkedList<String>(csStrings.Split(','));

		//Act
		linkedList.Clear();

		//Assert
		linkedList.IsEmpty.Should().BeTrue();
		linkedList.Count.Should().Be(0);
		linkedList.Head.Should().BeNull().And.Be(linkedList.Tail);
	}

	[Theory]
	[InlineData("-99,-1,0,1,99")]
	[InlineData("-99,-1,0")]
	[InlineData("-99,-1")]
	[InlineData("-99")]
	public void Successfully_ClearsTheListOfTypeInt32_WhenItOneOrMoreElements(String csInt32s)
	{
		//Arrange
		var i32Array = GetInt32Array(csInt32s);
		var linkedList = new DoublyCircularLinkedList<Int32>(i32Array);

		//Act
		linkedList.Clear();

		//Assert
		linkedList.IsEmpty.Should().BeTrue();
		linkedList.Count.Should().Be(0);
		linkedList.Head.Should().BeNull().And.Be(linkedList.Tail);
	}
}
