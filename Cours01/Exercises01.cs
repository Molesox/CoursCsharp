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
            return false;
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
            /*
            Expected Output: 
            0 -> "T = 32F"
            -300 -> "Temperature cannot be below absolute 0!"
            28.5 -> "T = 83.30F" 
            
            */
       
            return string.Empty;
        }

        /// <summary>
        /// Determines whether an array of POSITIV integers is sorted in ascending order.
        /// </summary>
        /// <param name="arr">The array of integers to check.</param>
        /// <returns>True if the array is sorted in ascending order, false otherwise.</returns>
        public static bool IsSortedAscending(int[] arr)
        {
            return true;
        }

        /// <summary>
        /// Draws 3 Christmas tree shapes using asterisks.
        /// </summary>
        /// <returns>A string representing the Christmas tree shape.</returns>
        public static string DrawChristmasTree()
        {
            /*
             - Expected Output:
             -    *
             -   ***
             -  *****
             - *******
             -    *
             -   ***
             -  *****
             - *******
             -    *
             -   ***
             -  *****
             - *******
             */

            return string.Empty;
        }

    }
}