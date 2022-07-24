namespace Dsa.Linear.UnitTests.Generics.LinkedList.Node;

using Dsa.Linear.Generics.LinkedList;
using FluentAssertions;
using System;
using Xunit;

public class IEquatable_EqualsShould
{
	[Fact]
	public void NotThrow_NotImplementedException()
	{
		//Arrange
		var nodeLeft = new Node<Int32>(1);
		var nodeRight = (Node<Int32>)null!;

		//Act
		var act = () => nodeLeft.Equals(nodeRight);

		//Assert
		act.Should().NotThrow<NotImplementedException>();
	}

	[Fact]
	public void ReturnFalse_WhenTypeIsInt32_AndRightNodeIsNull_ButLeftNodeIsNotNull()
	{
		//Arrange
		var nodeLeft = new Node<Int32>(1);
		var nodeRight = (Node<Int32>)null!;

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
	public void ReturnFalse_WhenTypeIsInt32_AndKeyOfTheNodesDoesNotMatches(
		Int32 leftKey,
		Int32 rightKey)
	{
		//Arrange
		var nodeLeft = new Node<Int32>(leftKey);
		var nodeRight = new Node<Int32>(rightKey);

		//Act
		var actual = nodeLeft.Equals(nodeRight);

		//Assert
		actual.Should().BeFalse();
	}

	[Theory]
	[InlineData("1", "2")]
	[InlineData("-1", "-2")]
	[InlineData("2147483647", "-2147483648")]
	[InlineData("", null)]
	[InlineData(null, "")]
	public void ReturnFalse_WhenTypeIsString_AndKeyOfTheNodesDoesNotMatches(
		String leftKey,
		String rightKey)
	{
		//Arrange
		var nodeLeft = new Node<String>(leftKey);
		var nodeRight = new Node<String>(rightKey);

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
	public void ReturnTrue_WhenTypeIsInt32_AndKeyOfTheNodesMatches(
		Int32 leftKey,
		Int32 rightKey)
	{
		//Arrange
		var nodeLeft = new Node<Int32>(leftKey);
		var nodeRight = new Node<Int32>(rightKey);

		//Act
		var actual = nodeLeft.Equals(nodeRight);

		//Assert
		actual.Should().BeTrue();
	}

	[Theory]
	[InlineData("", "")]
	[InlineData(null, null)]
	[InlineData("0", "0")]
	[InlineData("2147483647", "2147483647")]
	[InlineData("-2147483648", "-2147483648")]
	public void ReturnTrue_WhenTypeIsString_AndKeyOfTheNodesMatches(
		String leftKey,
		String rightKey)
	{
		//Arrange
		var nodeLeft = new Node<String>(leftKey);
		var nodeRight = new Node<String>(rightKey);

		//Act
		var actual = nodeLeft.Equals(nodeRight);

		//Assert
		actual.Should().BeTrue();
	}
}
