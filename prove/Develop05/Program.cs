using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        MindfulnessApp app = new MindfulnessApp();
        app.Run();
    }
}

class MindfulnessApp
{
    private BreathingActivity breathingActivity;
    private ReflectingActivity reflectingActivity;
    private ListingActivity listingActivity;

    public MindfulnessApp()
    {
        breathingActivity = new BreathingActivity();
        reflectingActivity = new ReflectingActivity();
        listingActivity = new ListingActivity();
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("menu options:");
            Console.WriteLine("1. start breathing activity");
            Console.WriteLine("2. start reflecting activity");
            Console.WriteLine("3. start listing activity");
            Console.WriteLine("4. quit");
            Console.Write("select a choice from the menu: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                breathingActivity.Start();
            }
            else if (choice == "2")
            {
                reflectingActivity.Start();
            }
            else if (choice == "3")
            {
                listingActivity.Start();
            }
            else if (choice == "4")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please select again.");
            }
        }
    }
}

abstract class Activity
{
    protected string name;
    protected string description;
    protected int duration;

    public void DisplayStartMessage()
    {
        Console.WriteLine($"Welcome to the {name} activity.");
        Console.WriteLine(description);
        Console.Write("how long, in seconds, would you like for your session? ");
        duration = int.Parse(Console.ReadLine());
        Console.WriteLine("get ready...");
        Pause(3);
    }

    public void DisplayEndMessage()
    {
        Console.WriteLine("well done!!");
        Console.WriteLine($"you have completed another {duration} seconds of the {name} activity.");
        Pause(3);
    }

    protected void Pause(int seconds)
    {
        Thread.Sleep(seconds * 1000);
    }
}

class BreathingActivity : Activity
{
    public BreathingActivity()
    {
        name = "breathing";
        description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
    }

    public void Start()
    {
        DisplayStartMessage();
        for (int i = 0; i < duration / 4; i++)
        {
            Console.WriteLine("breathe in...");
            Pause(2);
            Console.WriteLine("now breathe out...");
            Pause(2);
        }
        DisplayEndMessage();
    }
}

class ReflectingActivity : Activity
{
    private List<string> prompts = new List<string>
    {
        "think of a time when you did something really difficult."
    };

    private List<string> questions = new List<string>
    {
        "how did you feel when it was complete?",
        "what is your favorite thing about this experience?"
    };

    public ReflectingActivity()
    {
        name = "reflecting";
        description = "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.";
    }

    public void Start()
    {
        DisplayStartMessage();
        Console.WriteLine("consider the following prompt:");
        Console.WriteLine($"---{prompts[0]}---");
        Console.WriteLine("when you have something in mind, press enter to continue.");
        Console.ReadLine();

        Console.WriteLine("Now ponder on each of the following questions as they relate to this experience. You may begin in:");
        foreach (var question in questions)
        {
            Console.WriteLine($"> {question}");
            Pause(2); // Pause for user to reflect
        }

        DisplayEndMessage();
    }
}

class ListingActivity : Activity
{
    private List<string> prompts = new List<string>
    {
        "when you felt the Holy Ghost this month?"
    };

    public ListingActivity()
    {
        name = "listing";
        description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
    }

    public void Start()
    {
        DisplayStartMessage();
        Console.WriteLine("list as many responses you can to the following prompt:");
        Console.WriteLine($"---{prompts[0]}---");
        Console.WriteLine("You may begin in:");

        int count = 0;
        while (duration > 0)
        {
            string response = Console.ReadLine();
            count++;
            duration--; // Decrease duration for each response
        }

        Console.WriteLine($"you listed {count} items!");
        DisplayEndMessage();
    }
}
