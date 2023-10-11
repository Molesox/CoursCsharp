namespace ExercisesEvaluator;

public class UnitTestExercises01
{
    [Fact]
    public void Test1()
    {
        var result = Exercises.IsPair(3);
        Assert.False(result, "3 should not be pair");
    }
}