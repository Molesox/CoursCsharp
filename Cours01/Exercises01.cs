namespace Cours01
{
    public static class Exercises01
    {

        /// <summary>
        /// Determines if a given integer is an even number.
        /// </summary>
        /// <param name="number">The integer to check for parity.</param>
        /// <returns>True if the number is even, false otherwise.</returns>
        public static bool IsEven(int number)
        {
            return number % 2 == 0;
        }

        /// <summary>
        /// Converts a temperature in Celsius degrees to Fahrenheit degrees.
        /// Ensure the temperature is above -271.15°C (absolute zero).
        /// Max precision is 2 decimals
        /// </summary>
        /// <param name="celsius">Temperature in Celsius degrees.</param>
        /// <returns>The temperature in Fahrenheit with a formatted result, or an error message for invalid input.</returns>
        public static string CelsiusToFahrenheit(double celsius)
        {
            double fahrenheit;

            if (celsius < -273.15) return "Temperature below absolute zero!";


            fahrenheit = celsius * 1.8 + 32.0;

            return $"T = {fahrenheit:F2}F";

        }

        /// <summary>
        /// Determines whether an array of POSITIV integers is sorted in ascending order.
        /// </summary>
        /// <param name="arr">The array of integers to check.</param>
        /// <returns>True if the array is sorted in ascending order, false otherwise.</returns>
        public static bool IsSortedAscending(int[] arr)
        {
            if (arr is null || arr.Length <= 0) return false;

            int previous = -1;
            foreach (var val in arr)
            {
                if (val >= previous && val > 0)
                {
                    previous = val;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Draws 3 Christmas tree shapes using asterisks.
        /// </summary>
        /// <returns>A string representing the Christmas tree shape.</returns>
        public static void DrawChristmasTree(int treeWidth, int repeatCount)
        {
            for (var i = 0; i < repeatCount; i++)
            {
                for (var j = 0; j < treeWidth; j += 2)
                {
                    int spaces = (treeWidth - j) / 2;

                    for (var k = 0; k < spaces; k++)
                    {
                        Console.Write(" ");
                    }

                    for (var m = 0; m <= j; m++)
                    {
                        Console.Write("*");
                    }

                    for (var n = (treeWidth - j) / 2; n < treeWidth; n++)
                    {
                        Console.Write(" ");
                    }

                    Console.WriteLine();
                }
            }
        }


    }
}