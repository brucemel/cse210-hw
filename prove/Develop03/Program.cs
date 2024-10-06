using System;

class Program
{
    static void Main(string[] args)
    {
        Reference reference = new Reference("John", 3, 16);
        Scripture scripture = new Scripture(reference, "For God so loved the world that he gave his one and only Son," + Environment.NewLine + "that whoever believes in him shall not perish but have eternal life.");

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();
            Console.WriteLine("Press Enter to hide words or type 'quit' to exit.");

            string input = Console.ReadLine();
            if (input.ToLower() == "quit") break;

            scripture.HideRandomWords(3); // Number of words to hide
            if (scripture.IsCompletelyHidden())
            {
                Console.Clear();
                break;
            }
        }
    }
}

