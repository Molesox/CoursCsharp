using System.Runtime.CompilerServices;

namespace ExercisesEvaluator;

public class UnitTestExercises01
{
    [Theory]
    [InlineData(3, false)]
    [InlineData(4, true)]
    [InlineData(0, true)]
    [InlineData(-2, true)]
    [InlineData(7, false)]
    public void TestIsPair(int number, bool expectedResult)
    {
        var result = Exercises01.IsEven(number);
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(0, "T = 32F")] // 0 degrees Celsius should be 32 degrees Fahrenheit
    [InlineData(-300, "Temperature below absolute zero!")] // Below absolute zero
    [InlineData(28.5, "T = 83.30F")] // 28.5 degrees Celsius should be 83.3 degrees Fahrenheit
    public void TestCelsiusToFahr(double celsius, string expected)
    {
       var result = Exercises01.CelsiusToFahrenheit(celsius);

        // Assert
        Assert.Equal(expected, result); 
    }

    [Theory]
    [InlineData(new int[] { 1, 8, 7 }, false)]
    [InlineData(new int[] { -4, -3, -2 }, false)]
    [InlineData(new int[] { }, false)]
    [InlineData(null, false)]
    [InlineData(new int[] { 1 }, true)]
    [InlineData(new int[] { 1, 2, 7 }, true)]
    [InlineData(new int[] { 8, 9999 }, true)]
    public void TestSorting(int[] value, bool expectedResult)
    {
        var result = Exercises01.IsSortedAscending(value);
        Assert.Equal(result, expectedResult);
    }



    [Fact]
    public void TestChristmasTree()
    {
        var result = Exercises01.DrawChristmasTree();
        var expected = "   *    \n  ***     \n *****      \n*******       \n   *    \n  ***     \n *****      \n*******       \n   *    \n  ***     \n *****      \n*******       \n";
        Assert.Equal(expected, result);
    }
}