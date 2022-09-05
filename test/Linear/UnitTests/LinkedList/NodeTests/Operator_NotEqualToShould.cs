namespace Dsa.Linear.UnitTests.LinkedList.NodeTests;

using Dsa.Linear.LinkedList.Doubly;

public class Operator_NotEqualToShould
{
	[Fact]
	public void NotThrow_NotImplementedException()
	{
		//Arrange
		var nodeLeft = new Node(1);
		var nodeRight = (Node)null!;

		//Act
		var act = () => nodeLeft == nodeRight;

		//Assert
		act.Should().NotThrow<NotImplementedException>();
	}

	[Fact]
	public void ReturnFalse_WhenBothNodesAreNull()
	{
		//Arrange
		Node nodeLeft = null!;
		var nodeRight = (Node)null!;

		//Act
		var actual = nodeLeft != nodeRight;

		//Assert
		actual.Should().BeFalse();
	}

	[Theory]
	[InlineData(2, 2)]
	[InlineData(-2, -2)]
	[InlineData(0, 0)]
	[InlineData(Int32.MaxValue, Int32.MaxValue)]
	[InlineData(Int32.MinValue, Int32.MinValue)]
	public void ReturnFalse_WhenKeyOfTheNodesMatches(
		Int32 leftKey,
		Int32 rightKey)
	{
		//Arrange
		var nodeLeft = new Node(leftKey);
		var nodeRight = new Node(rightKey);

		//Act
		var actual = nodeLeft != nodeRight;

		//Assert
		actual.Should().BeFalse();
	}

	[Fact]
	public void ReturnTrue_WhenRightNodeIsNull_AndLeftNodeIsNotNull()
	{
		//Arrange
		var nodeLeft = new Node(1);
		var nodeRight = (Node)null!;

		//Act
		var actual = nodeLeft != nodeRight;

		//Assert
		actual.Should().BeTrue();
	}

	[Fact]
	public void ReturnTrue_WhenRightNodeIsNotNull_AndLeftNodeIsNull()
	{
		//Arrange
		var nodeLeft = (Node)null!;
		var nodeRight = new Node(1);

		//Act
		var actual = nodeLeft != nodeRight;

		//Assert
		actual.Should().BeTrue();
	}

	[Theory]
	[InlineData(1, 2)]
	[InlineData(-1, -2)]
	[InlineData(0, 5)]
	[InlineData(Int32.MaxValue, Int32.MinValue)]
	public void ReturnTrue_WhenKeyOfTheNodesDoesNotMatches(
		Int32 leftKey,
		Int32 rightKey)
	{
		//Arrange
		var nodeLeft = new Node(leftKey);
		var nodeRight = new Node(rightKey);

		//Act
		var actual = nodeLeft != nodeRight;

		//Assert
		actual.Should().BeTrue();
	}
}
