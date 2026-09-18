namespace Y2021D01.Tests;

public class SonarTest
{
    /*
     * 199 (N/A - no previous measurement)
       200 (increased)
       208 (increased)
       210 (increased)
       200 (decreased)
       207 (increased)
       240 (increased)
       269 (increased)
       260 (decreased)
       263 (increased)
     */
    
    
    [Theory]
    [InlineData(new int[] {199, 200, 208, 210, 200, 207, 240, 269, 260, 263}, 7)]
    [InlineData(new int[] {1,2,3,1,1,1,1}, 2)]
    [InlineData(new int[] {1,2,3,-5,0,0,0,0,-2,-5}, 3)]
    [InlineData(new int[] {2,2,2,2,2,2}, 0)]
    [InlineData(new int[] {}, 0)]
    public void CountIncreases_KnownArrays_ReturnsExpected(int[] depths, int expected)
    {
        // Arrange
        var sut = new Sonar();

        // Act
        var actual = sut.CountIncreases(depths);

        // Assert
        Assert.Equal(expected, actual);
    }
}