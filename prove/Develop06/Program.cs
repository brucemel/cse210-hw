using System;
using System.Collections.Generic;
using System.IO;

public class QuestProgram
{
    private static List<Achievement> achievements = new List<Achievement>();
    private static int totalRewards = 0;

    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine($"You have {totalRewards} points.\n");
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");
            Console.Write("Select a choice from the menu: ");
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
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("1. Simple goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Which type of goal would you like to create? ");
        string type = Console.ReadLine();
        Console.Write("What is the name of your goal? ");
        string title = Console.ReadLine();
        Console.Write("What is a short description of it? ");
        string details = Console.ReadLine();
        Console.Write("What is the amount of points associated with this goal? ");
        if (!int.TryParse(Console.ReadLine(), out int points))
        {
            Console.WriteLine("Invalid points value. Please enter a valid number.");
            return;
        }

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
                if (!int.TryParse(Console.ReadLine(), out int requiredCompletions))
                {
                    Console.WriteLine("Invalid completions value. Please enter a valid number.");
                    return;
                }
                Console.Write("What is the bonus reward for completing it? ");
                if (!int.TryParse(Console.ReadLine(), out int bonusPoints))
                {
                    Console.WriteLine("Invalid bonus points value. Please enter a valid number.");
                    return;
                }
                newAchievement = new MilestoneAchievement(title, details, points, requiredCompletions, bonusPoints);
                break;
            default:
                Console.WriteLine("Invalid goal type.");
                return;
        }

        achievements.Add(newAchievement);
        Console.WriteLine("Goal added successfully!");
    }

    private static void ShowAchievements()
    {
        Console.WriteLine("The goals are:");
        for (int i = 0; i < achievements.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {achievements[i].GetDetails()}");
        }
    }

    private static void SaveAchievements()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var achievement in achievements)
            {
                writer.WriteLine(achievement.ToString());
            }
        }
        Console.WriteLine("Goals saved successfully!");
    }

    private static void LoadAchievements()
    {
        Console.Write("What is the filename for the goal file? ");
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
            Console.WriteLine("Goals loaded successfully!");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }

    private static void CompleteAchievement()
    {
        Console.WriteLine("The goals are:");
        for (int i = 0; i < achievements.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {achievements[i].GetTitle()}");
        }
        Console.Write("Which goal did you accomplish? ");
        if (!int.TryParse(Console.ReadLine(), out int achievementIndex) || achievementIndex < 1 || achievementIndex > achievements.Count)
        {
            Console.WriteLine("Invalid goal selection.");
            return;
        }

        achievements[achievementIndex - 1].RecordCompletion();
        totalRewards += achievements[achievementIndex - 1].GetRewardPoints();
        Console.WriteLine($"congratulations! you have earned {achievements[achievementIndex - 1].GetRewardPoints()} points!");
        Console.WriteLine($"You now have {totalRewards} points.");
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
        Console.WriteLine($"Congratulations! You've completed the simple goal '{title}' and earned {rewardPoints} points!");
    }

    public override string GetDetails() => $"{title} ({description})";
}

public class OngoingAchievement : Achievement
{
    public OngoingAchievement(string title, string description, int rewardPoints)
        : base(title, description, rewardPoints) { }

    public override void RecordCompletion()
    {
        Console.WriteLine($"Great job! You've completed the ongoing goal '{title}' and earned {rewardPoints} points!");
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
            Console.WriteLine($"Congratulations! You've completed the milestone goal '{title}' and earned {pointsGained} points with a bonus!");
        }
        else
        {
            Console.WriteLine($"You've completed a part of the milestone goal '{title}' and earned {rewardPoints} points. Current progress: {currentCompletions}/{completionsRequired}");
        }
    }

    public override string GetDetails() => $"{title} ({description}) -- Current completions: {currentCompletions}/{completionsRequired}";
}
