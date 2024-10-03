using System;

public class Fraction
{
    private int numerator;
    private int denominator;

    // Constructor with no parameters that initializes to 1/1
    public Fraction()
    {
        numerator = 1;
        denominator = 1;
    }

    // Constructor that initializes the numerator and sets the denominator to 1
    public Fraction(int num)
    {
        numerator = num;
        denominator = 1;
    }

    // Constructor that initializes both the numerator and the denominator
    public Fraction(int num, int denom)
    {
        if (denom == 0)
        {
            throw new ArgumentException("Denominator cannot be zero.");
        }
        numerator = num;
        denominator = denom;
    }

    // Getter and Setter for the numerator
    public int GetNumerator() => numerator;
    public void SetNumerator(int num) => numerator = num;

    // Getter and Setter for the denominator
    public int GetDenominator() => denominator;
    public void SetDenominator(int denom)
    {
        if (denom == 0)
        {
            throw new ArgumentException("Denominator cannot be zero.");
        }
        denominator = denom;
    }

    // Method that returns the fraction as a string
    public string GetFractionString() => $"{numerator}/{denominator}";

    // Method that returns the decimal value of the fraction
    public double GetDecimalValue() => (double)numerator / denominator;
}

class Program
{
    static void Main(string[] args)
    {
        // Create fractions using the specified constructors
        Fraction fraction1 = new Fraction();       // Default: 1/1
        Fraction fraction2 = new Fraction(5);      // 5/1
        Fraction fraction3 = new Fraction(3, 4);   // 3/4
        Fraction fraction4 = new Fraction(1, 3);   // 1/3

        // Display the fractions and their decimal values
        Console.WriteLine(fraction1.GetFractionString()); // Output: 1/1
        Console.WriteLine(fraction1.GetDecimalValue());   // Output: 1.0

        Console.WriteLine(fraction2.GetFractionString()); // Output: 5/1
        Console.WriteLine(fraction2.GetDecimalValue());   // Output: 5.0

        Console.WriteLine(fraction3.GetFractionString()); // Output: 3/4
        Console.WriteLine(fraction3.GetDecimalValue());   // Output: 0.75

        Console.WriteLine(fraction4.GetFractionString()); // Output: 1/3
        Console.WriteLine(fraction4.GetDecimalValue());   // Output: 0.3333333333333333
    }
}
