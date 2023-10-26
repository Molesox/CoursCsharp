namespace Cours02
{
    /// <summary>
    /// Exemple polymorphisme ad hoc 
    /// </summary>
    public class Calculator
    {
        // Surcharge #1: Addition de deux entiers
        public int Add(int a, int b)
        {
            return a + b;
        }

        // Surcharge #2: Addition de trois entiers
        public int Add(int a, int b, int c)
        {
            return a + b + c;
        }

        // Surcharge #3: Addition de deux doubles
        public double Add(double a, double b)
        {
            return a + b;
        }
    }
}
