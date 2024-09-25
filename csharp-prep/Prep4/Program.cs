using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        while (true)
        {
            Console.Write("Enter number: ");
            int num = int.Parse(Console.ReadLine());
            if (num == 0)
            {
                break;
            }
            numbers.Add(num);
        }

        // Calcular la suma
        int totalSum = 0;
        foreach (int number in numbers)
        {
            totalSum += number;
        }

        // Calcular el promedio
        double average = (double)totalSum / numbers.Count;

        // Encontrar el número más grande
        int largest = int.MinValue;
        foreach (int number in numbers)
        {
            if (number > largest)
            {
                largest = number;
            }
        }

        // Encontrar el menor número positivo
        int? smallestPositive = null;
        foreach (int number in numbers)
        {
            if (number > 0 && (smallestPositive == null || number < smallestPositive))
            {
                smallestPositive = number;
            }
        }

        // Ordenar la lista
        numbers.Sort();

        // Imprimir resultados
        Console.WriteLine($"The sum is: {totalSum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {largest}");
        
        if (smallestPositive.HasValue)
        {
            Console.WriteLine($"The smallest positive number is: {smallestPositive}");
        }
        else
        {
            Console.WriteLine("There are no positive numbers.");
        }

        Console.WriteLine("The sorted list is:");
        foreach (int number in numbers)
        {
            Console.WriteLine(number);
        }
    }
}

