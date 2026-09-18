using AdventOfCode.Common;

namespace Common.Tests;

public class InputTests
{
    [Fact]
    public void Lines_SplitsOnNewline()
    {
        // Arrange
        var text = "abc\ndef\nghi";

        // Act
        var actual = Input.Lines(text);

        // Assert
        Assert.Equal(new[] { "abc", "def", "ghi" }, actual);
    }

    [Fact]
    public void Lines_TrimsWhitespaceAndWindowsLineEndings()
    {
        // Arrange
        var text = "  abc \r\ndef\r\n";

        // Act
        var actual = Input.Lines(text);

        // Assert
        Assert.Equal(new[] { "abc", "def" }, actual);
    }

    [Fact]
    public void Lines_SkipsEmptyLines()
    {
        // Arrange
        var text = "abc\n\n\ndef\n";

        // Act
        var actual = Input.Lines(text);

        // Assert
        Assert.Equal(new[] { "abc", "def" }, actual);
    }

    [Fact]
    public void Lines_OfEmptyText_IsEmpty()
    {
        // Arrange
        // Act
        var actual = Input.Lines("");

        // Assert
        Assert.Empty(actual);
    }

    [Theory]
    [InlineData("1 2 3", new[] { 1, 2, 3 })]
    [InlineData("16,1,2", new[] { 16, 1, 2 })]
    [InlineData("2x3x4", new[] { 2, 3, 4 })]
    [InlineData("199\n200\n208", new[] { 199, 200, 208 })]
    [InlineData("-5 10", new[] { -5, 10 })]
    public void Numbers_SplitsOnAnySeparator(string text, int[] expected)
    {
        // Arrange
        // Act
        var actual = Input.Numbers(text);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Numbers_OfEmptyText_IsEmpty()
    {
        // Arrange
        // Act
        var actual = Input.Numbers("   \n  ");

        // Assert
        Assert.Empty(actual);
    }
}
