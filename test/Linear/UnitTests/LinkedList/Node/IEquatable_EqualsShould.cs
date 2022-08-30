namespace Dsa.Linear.UnitTests.LinkedList.Node;

using Dsa.Linear.LinkedList.Doubly;

public class IEquatable_EqualsShould
{
	[Fact]
	public void NotThrow_NotImplementedException()
	{
		//Arrange
		var nodeLeft = new Node(1);
		var nodeRight = (Node)null!;

		//Act
		var act = () => nodeLeft.Equals(nodeRight);

		//Assert
		act.Should().NotThrow<NotImplementedException>();
	}

	[Fact]
	public void ReturnsFalse_WhenTheNodeBeingPassedAsAnArgumentIsNull()
	{
		//Arrange
		var nodeLeft = new Node(1);
		var nodeRight = (Node)null!;

		//Act
		var actual = nodeLeft.Equals(nodeRight);

		//Assert
		actual.Should().BeFalse();
	}

	[Theory]
	[InlineData(1, 2)]
	[InlineData(-1, -2)]
	[InlineData(0, 5)]
	[InlineData(Int32.MaxValue, Int32.MinValue)]
	public void ReturnFalse_WhenKeyOfTheNodesDoesNotMatches(
		Int32 leftKey,
		Int32 rightKey)
	{
		//Arrange
		var nodeLeft = new Node(leftKey);
		var nodeRight = new Node(rightKey);

		//Act
		var actual = nodeLeft.Equals(nodeRight);

		//Assert
		actual.Should().BeFalse();
	}

	[Theory]
	[InlineData(2, 2)]
	[InlineData(-2, -2)]
	[InlineData(0, 0)]
	[InlineData(Int32.MaxValue, Int32.MaxValue)]
	[InlineData(Int32.MinValue, Int32.MinValue)]
	public void ReturnTrue_WhenKeyOfTheNodesMatches(
		Int32 leftKey,
		Int32 rightKey)
	{
		//Arrange
		var nodeLeft = new Node(leftKey);
		var nodeRight = new Node(rightKey);

		//Act
		var actual = nodeLeft.Equals(nodeRight);

		//Assert
		actual.Should().BeTrue();
	}
}
