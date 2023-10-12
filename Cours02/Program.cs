using Cours02;
Calculator calc = new Calculator();

Console.WriteLine(calc.Add(2, 3));      // Affiche 5 (en utilisant la surcharge #1)
Console.WriteLine(calc.Add(2, 3, 4));   // Affiche 9 (en utilisant la surcharge #2)
Console.WriteLine(calc.Add(2.5, 3.5));  // Affiche 6.0 (en utilisant la surcharge #3)