namespace Dsa.Linear.UnitTests.LinkedList.NodeTest;

using Dsa.Linear.LinkedList.Doubly;

public class IComparable_CompareToShould
{
	[Fact]
	public void NotThrow_NotImplementedException()
	{
		//Arrange
		var nodeLeft = new Node(1);
		var nodeRight = (Node)null!;

		//Act
		var act = () => nodeLeft.CompareTo(nodeRight);

		//Assert
		act.Should().NotThrow<NotImplementedException>();
	}

	[Fact]
	public void Throw_ArgumentNullException_WhenArgumentPassedIsNull()
	{
		//Arrange
		var paramName = "node";
		var nodeLeft = new Node(1);
		var nodeRight = (Node)null!;

		//Act
		var act = () => nodeLeft.CompareTo(nodeRight);

		//Assert
		act.Should()
			.Throw<ArgumentNullException>()
			.WithMessage($"Value cannot be null. (Parameter '{paramName}')")
			.WithParameterName(paramName);
	}

	[Theory]
	[InlineData(10, 2, 1)]
	[InlineData(Int32.MaxValue, Int32.MinValue, 1)]
	[InlineData(-9, -1, -1)]
	[InlineData(Int32.MinValue, Int32.MaxValue, -1)]
	[InlineData(0, 0, 0)]
	[InlineData(Int32.MaxValue, Int32.MaxValue, 0)]
	[InlineData(Int32.MinValue, Int32.MinValue, 0)]
	public void ReturnsInt32Value_BasedOnComparisonOfTheKeyOfTwoNodeInstances(
		Int32 instanceKey,
		Int32 argumentKey,
		Int32 expectedValue)
	{
		//Arrange
		var nodeUsedAsInstance = new Node(instanceKey);
		var nodeUsedAsArgument = new Node(argumentKey);

		//Act
		var actualValue = nodeUsedAsInstance.CompareTo(nodeUsedAsArgument);

		//Assert
		actualValue.Should().Be(expectedValue);
	}
}
