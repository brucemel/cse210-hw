using System;

class Program
{
    static void Main(string[] args)
    {
        Random randomGenerator = new Random();
        int number = randomGenerator.Next(1, 101); // Generate a number between 1 and 100

        int guess = 0; // Initialize guess

        // Loop until the user guesses the correct number
        while (guess != number)
        {
            Console.Write("What is your guess? ");

            // Validate user input
            if (int.TryParse(Console.ReadLine(), out guess))
            {
                if (guess == number)
                {
                    Console.WriteLine("You did it!");
                }
                else
                {
                    Console.WriteLine("Sorry, try again!");
                    if (guess >= number)
                    {
                        Console.WriteLine("Lower");
                    }
                    else
                    {
                        Console.WriteLine("Higher");
                    }
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid number.");
            }
        }
    }
}
