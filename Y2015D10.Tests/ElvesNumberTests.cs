namespace Y2015D10.Tests;

public class ElvesNumberTests
{
    [Theory]
    [InlineData("1", "11")]
    public void ElvesNumber_Numbers_AreCountedCorrectly(string input, string expected)
    {
        // Arrange
        var sut = new ElvesNumbers();

        // Act
        var actual = sut.ReocurringNumbers(input);

        // Assert
        Assert.Equal(expected, actual);
    }
}