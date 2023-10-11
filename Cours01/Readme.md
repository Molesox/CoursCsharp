# Cours01 Syslog C# - dotnet
Voici le document montré lors du premier cours.


1. **Print to console**
    ```csharp
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
    ```

2. **String and String Interpolation**
    ```csharp
    static void Main(string[] args)
    {
        string firstName = "John";
        string lastName = "Doe";
        
        Console.WriteLine("First Name: " + firstName);
        Console.WriteLine("Last Name: " + lastName);
        Console.WriteLine($"Full Name: {firstName} {lastName}");
    }
    ```

3. **Using Booleans**
    ```csharp
    static void Main(string[] args)
    {
        bool isRaining = false;
        
        if (isRaining)
        {
            Console.WriteLine("It's raining. Take an umbrella!");
        }
        else
        {
            Console.WriteLine("It's not raining. Have a great day!");
        }
    }
    ```

4. **Using uint, int,  long...**
    ```csharp
    static void Main(string[] args)
    {
        // Integral types
        byte smallUnsignedByte = 255;            // 0 to 255
        sbyte smallSignedByte = -127;            // -128 to 127
        ushort unsignedShort = 65000;            // 0 to 65,535
        short signedShort = -32000;              // -32,768 to 32,767
        uint unsignedNumber = 4000000000;        // 0 to 4,294,967,295
        int signedNumber = -2000000000;          // -2,147,483,648 to 2,147,483,647
        ulong largeUnsignedNumber = 18000000000000000000UL; // 0 to 18,446,744,073,709,551,615
        long largeNumber = 5000000000L;          // -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807
        
        // Real number types
        float singlePrecision = 3.14F;           // Approximately ±1.5 x 10^−45 to ±3.4 x 10^38
        double doublePrecision = 3.141592653589793; // Approximately ±5.0 x 10^−324 to ±1.7 x 10^308
        decimal highPrecision = 3.1415926535897932384626433832M; // Approximately ±1.0 x 10^−28 to ±7.9 x 10^28

        // Display the values
        Console.WriteLine($"Byte: {smallUnsignedByte}");
        Console.WriteLine($"SByte: {smallSignedByte}");
        Console.WriteLine($"UShort: {unsignedShort}");
        Console.WriteLine($"Short: {signedShort}");
        Console.WriteLine($"UInt: {unsignedNumber}");
        Console.WriteLine($"Int: {signedNumber}");
        Console.WriteLine($"ULong: {largeUnsignedNumber}");
        Console.WriteLine($"Long: {largeNumber}");
        Console.WriteLine($"Float: {singlePrecision}");
        Console.WriteLine($"Double: {doublePrecision}");
        Console.WriteLine($"Decimal: {highPrecision}");
    }
    ```
    5. **Operators**
    ```c#
    static void Main(string[] args)
    {
        // Arithmetic Operators
        int a = 5, b = 3;
        Console.WriteLine($"Addition: {a + b}");         // Result: 8
        Console.WriteLine($"Subtraction: {a - b}");      // Result: 2
        Console.WriteLine($"Multiplication: {a * b}");   // Result: 15
        Console.WriteLine($"Division: {a / b}");         // Result: 1
        Console.WriteLine($"Modulus: {a % b}");          // Result: 2

        // Relational Operators
        Console.WriteLine($"a == b: {a == b}");          // Result: false
        Console.WriteLine($"a != b: {a != b}");          // Result: true
        Console.WriteLine($"a > b: {a > b}");            // Result: true

        // Logical Operators
        bool x = true, y = false;
        Console.WriteLine($"x && y: {x && y}");          // Result: false
        Console.WriteLine($"x || y: {x || y}");          // Result: true
        Console.WriteLine($"!x: {!x}");                  // Result: false

        // Assignment Operators
        int c;
        c = a + b;
        Console.WriteLine($"c = a + b: {c}");            // Result: 8
        c += a;
        Console.WriteLine($"c += a: {c}");               // Result: 13

        // Unary Operators
        a++;
        Console.WriteLine($"Incremented a: {a}");        // Result: 6
        b--;
        Console.WriteLine($"Decremented b: {b}");        // Result: 2
    }

    ```

6. **Reading Input from the User**
    ```csharp
    static void Main(string[] args)
    {
        Console.Write("Enter your name: ");
        string name = Console.ReadLine();
        Console.WriteLine($"Hello, {name}!");
    }
    ```

7. **Using a Loop**
    ```csharp
    static void Main(string[] args)
    {
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"Number: {i}");
        }
    }
    ```

8. **Using Conditional Statements**
    ```csharp
    static void Main(string[] args)
    {
        Console.Write("Enter a number: ");
        int number = int.Parse(Console.ReadLine());

        if (number > 10)
        {
            Console.WriteLine("The number is greater than 10.");
        }
        else
        {
            Console.WriteLine("The number is 10 or less.");
        }
    }
    ```

9. **Working with Arrays**
    ```csharp
    static void Main(string[] args)
    {
        int[] numbers = { 1, 2, 3, 4, 5 };
        
        foreach (var num in numbers)
        {
            Console.WriteLine(num);
        }
    }
    ```

10. **Working with Lists**
    ```csharp
    using System.Collections.Generic;

    static void Main(string[] args)
    {
        List<string> fruits = new List<string> 
        { "apple", "banana", "cherry" };
        
        foreach (var fruit in fruits)
        {
            Console.WriteLine(fruit);
        }
    }
    ```

10. **Handling Exceptions**
    ```csharp
    static void Main(string[] args)
    {
        Console.Write("Enter a number: ");
        try
        {
            int number = int.Parse(Console.ReadLine());
            Console.WriteLine($"You entered: {number}");
        }
        catch (FormatException)
        {
            Console.WriteLine("That's not a valid number!");
        }
    }
    ```

11. **Using `enum` with `switch` for Strongly Typed Constants and Multi-way Branching**s
    ```c#
    enum Days
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }

    static void Main(string[] args)
    {
        Console.Write("Enter a day (e.g., Monday, Tuesday): ");
        string dayInput = Console.ReadLine();

        if (Enum.TryParse(typeof(Days), dayInput, true, out object day))
        {
            switch((Days)day)
            {
                case Days.Sunday:
                    Console.WriteLine("It's a relaxing day!");
                    break;
                case Days.Monday:
                    Console.WriteLine("Start of the workweek!");
                    break;
                case Days.Friday:
                    Console.WriteLine("Almost the weekend!");
                    break;
                default:
                    Console.WriteLine($"{dayInput} is just another day.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid day entered!");
        }
    }
    ```
