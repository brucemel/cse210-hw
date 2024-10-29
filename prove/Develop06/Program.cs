using System;
using System.Collections.Generic;
using System.IO;

public class QuestProgram
{
    private static List < Achievement> achievements = new List<Achievement>();
    private static int totalRewards = 0;

    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine($"You have {totalRewards} rewards.\n");
            Console.WriteLine("Main Menu:");
            Console.WriteLine("1. Add New Achievement");
            Console.WriteLine("2. Show Achievements");
            Console.WriteLine("3. Save Achievements");
            Console.WriteLine("4. Load Achievements");
            Console.WriteLine("5. Complete Achievement");
            Console.WriteLine("6. Exit");
            Console.Write("Select an option: ");
            string selection = Console.ReadLine();

            switch (selection)
            {
                case "1":
                    AddNewAchievement();
                    break;
                case "2":
                    ShowAchievements();
                    break;
                case "3":
                    SaveAchievements();
                    break;
                case "4":
                    LoadAchievements();
                    break;
                case "5":
                    CompleteAchievement();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }
    }

    private static void AddNewAchievement()
    {
        Console.WriteLine("Available Achievement Types:");
        Console.WriteLine("1. Simple Achievement");
        Console.WriteLine("2. Ongoing Achievement");
        Console.WriteLine("3. Milestone Achievement");
        Console.Write("Which type would you like to add? ");
        string type = Console.ReadLine();

        Console.Write("Enter the name of your achievement: ");
        string title = Console.ReadLine();

        Console.Write("Provide a brief description: ");
        string details = Console.ReadLine();

        Console.Write("Assign reward points for this achievement: ");
        int points = int.Parse(Console.ReadLine());

        Achievement newAchievement = null;

        switch (type)
        {
            case "1":
                newAchievement = new SimpleAchievement(title, details, points);
                break;
            case "2":
                newAchievement = new OngoingAchievement(title, details, points);
                break;
            case "3":
                Console.Write("How many completions are required? ");
                int requiredCompletions = int.Parse(Console.ReadLine());
                Console.Write("What is the bonus reward for completing it? ");
                int bonusPoints = int.Parse(Console.ReadLine());
                newAchievement = new MilestoneAchievement(title, details, points, requiredCompletions, bonusPoints);
                break;
            default:
                Console.WriteLine("Invalid achievement type.");
                return;
        }

        achievements.Add(newAchievement);
        Console.WriteLine("Achievement added successfully!");
    }

    private static void ShowAchievements()
    {
        Console.WriteLine("Current Achievements:");
        for (int i = 0; i < achievements.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {achievements[i].GetDetails()}");
        }
    }

    private static void SaveAchievements()
    {
        Console.Write("Enter the filename to save achievements: ");
        string filename = Console.ReadLine();

        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var achievement in achievements)
            {
                writer.WriteLine(achievement.ToString());
            }
        }

        Console.WriteLine("Achievements saved successfully!");
    }

    private static void LoadAchievements()
    {
        Console.Write("Enter the filename to load achievements: ");
        string filename = Console.ReadLine();

        if (File.Exists(filename))
        {
            achievements.Clear();
            string[] lines = File.ReadAllLines(filename);
            foreach (var line in lines)
            {
                string[] parts = line.Split(':');
                if (parts[0] == "SimpleAchievement")
                {
                    achievements.Add(new SimpleAchievement(parts[1], parts[2], int.Parse(parts[3])));
                }
                else if (parts[0] == "OngoingAchievement")
                {
                    achievements.Add(new OngoingAchievement(parts[1], parts[2], int.Parse(parts[3])));
                }
                else if (parts[0] == "MilestoneAchievement")
                {
                    achievements.Add(new MilestoneAchievement(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5])));
                }
            }

            Console.WriteLine("Achievements loaded successfully!");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }

    private static void CompleteAchievement()
    {
        Console.WriteLine("Available Achievements:");
        for (int i = 0; i < achievements.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {achievements[i].GetTitle()}");
        }

        Console.Write("Which achievement did you complete? ");
        int achievementIndex = int.Parse(Console.ReadLine()) - 1;

        if (achievementIndex >= 0 && achievementIndex < achievements.Count)
        {
            achievements[achievementIndex].RecordCompletion();
            totalRewards += achievements[achievementIndex].GetRewardPoints();
        }
        else
        {
            Console.WriteLine("Invalid achievement selection.");
        }
    }
}

public abstract class Achievement
{
    protected string title;
    protected string description;
    protected int rewardPoints;

    public Achievement(string title, string description, int rewardPoints)
    {
        this.title = title;
        this.description = description;
        this.rewardPoints = rewardPoints;
    }

    public abstract void RecordCompletion();
    public abstract string GetDetails();
    public string GetTitle() => title;
    public virtual int GetRewardPoints() => rewardPoints;

    public override string ToString() => $"{GetType().Name}:{title}:{description}:{rewardPoints}";
}

public class SimpleAchievement : Achievement
{
    public SimpleAchievement(string title, string description, int rewardPoints)
        : base(title, description, rewardPoints) { }

    public override void RecordCompletion()
    {
        Console.WriteLine($"Congratulations! You've completed the simple achievement '{title}' and earned {rewardPoints} points!");
    }

    public override string GetDetails() => $"{title} ({description})";
}

public class OngoingAchievement : Achievement
{
    public OngoingAchievement(string title, string description, int rewardPoints)
        : base(title, description, rewardPoints) { }

    public override void RecordCompletion()
    {
        Console.WriteLine($"Great job! You've completed the ongoing achievement '{title}' and earned {rewardPoints} points!");
    }

    public override string GetDetails() => $"{title} ({description})";
}

public class MilestoneAchievement : Achievement
{
    private int completionsRequired;
    private int bonusPoints;
    private int currentCompletions;

    public MilestoneAchievement(string title, string description, int rewardPoints, int completionsRequired, int bonusPoints)
        : base(title, description, rewardPoints)
    {
        this.completionsRequired = completionsRequired;
        this.bonusPoints = bonusPoints;
        this.currentCompletions = 0;
    }

    public override void RecordCompletion()
    {
        currentCompletions++;
        int pointsGained = rewardPoints;

        if (currentCompletions == completionsRequired)
        {
            pointsGained += bonusPoints;
            Console.WriteLine($"Congratulations! You've completed the milestone achievement '{title}' and earned {pointsGained} points with a bonus!");
        }
        else
        {
            Console.WriteLine($"You've completed a part of the milestone achievement '{title}' and earned {rewardPoints} points. Current progress: {currentCompletions}/{completionsRequired}");
        }
    }

    public override string GetDetails() => $"{title} ({description}) -- Current completions: {currentCompletions}/{completionsRequired}";
}
