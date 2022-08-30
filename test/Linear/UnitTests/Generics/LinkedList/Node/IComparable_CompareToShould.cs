namespace Dsa.Linear.UnitTests.Generics.LinkedList.Node;
public class IComparable_CompareToShould
{
	[Fact]
	public void NotThrow_NotImplementedException()
	{
		//Arrange
		var nodeLeft = new Node<Int32>(1);
		var nodeRight = (Node<Int32>)null!;

		//Act
		var act = () => nodeLeft.CompareTo(nodeRight);

		//Assert
		act.Should().NotThrow<NotImplementedException>();
	}

	[Fact]
	public void Throw_ArgumentNullException_WhenTypeIsInt32_AndArgumentPassedIsNull()
	{
		//Arrange
		var paramName = "node";
		var nodeLeft = new Node<Int32>(1);
		var nodeRight = (Node<Int32>)null!;

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
	public void ReturnsInt32Value_BasedOnComparisonOfTheKeyOfTwoNodeInstances_WhenTypeIsInt32(
		Int32 instanceKey,
		Int32 argumentKey,
		Int32 expectedValue)
	{
		//Arrange
		var nodeUsedAsInstance = new Node<Int32>(instanceKey);
		var nodeUsedAsArgument = new Node<Int32>(argumentKey);

		//Act
		var actualValue = nodeUsedAsInstance.CompareTo(nodeUsedAsArgument);

		//Assert
		actualValue.Should().Be(expectedValue);
	}

	[Theory]
	[InlineData("abc", "abc", 0)]
	[InlineData("abc", "bbc", -1)]
	[InlineData("abc", null, 1)]
	[InlineData("abc", "", 1)]
	[InlineData("bbc", "bac", 1)]
	public void ReturnsInt32Value_BasedOnComparisonOfTheKeyOfTwoNodeInstances_WhenTypeIsString(
		String instanceKey,
		String argumentKey,
		Int32 expectedValue)
	{
		//Arrange
		var nodeUsedAsInstance = new Node<String>(instanceKey);
		var nodeUsedAsArgument = new Node<String>(argumentKey);

		//Act
		var actualValue = nodeUsedAsInstance.CompareTo(nodeUsedAsArgument);

		//Assert
		actualValue.Should().Be(expectedValue);
	}
}
