namespace Y2015D01.Tests;

public class SantaFloorGuideTests
{
    [Theory]
    [InlineData("(())", 0)]
    [InlineData("()()", 0)]
    [InlineData("))(((((", 3)]
    [InlineData("())", -1)]
    [InlineData(")())())", -3)]
    [InlineData("()(()()))", -1)]
    public void SantaFloorGuide_EndsOnFloor_ReturnZero(string input, int expected)
    {
        // Arrange
        var sut = new SantaFloorGuide();

        // Act
        var actual = sut.CalculateFloor(input);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(")", 1)]
    [InlineData("()())",5)]
    [InlineData("((((())))))",11)]
    public void SantaFloorGuide_EndFloor_ReturnsOne(string input, int expected)
    {
        // Arrange
        var sut = new SantaFloorGuide();

        // Act
        var actual = sut.CalculateEndFloor(input);
        
        // Assert
        
        Assert.Equal(expected, actual);
    }
}