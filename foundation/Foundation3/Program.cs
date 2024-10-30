using System;
using System.Collections.Generic;

// Base class
public abstract class Activity
{
    private DateTime date;
    private int duration; // in minutes

    public Activity(DateTime date, int duration)
    {
        this.date = date;
        this.duration = duration;
    }

    // Public property to access the duration
    public int Duration => duration;

    public abstract double GetDistance(); // to be implemented by derived classes
    public abstract double GetSpeed(); // to be implemented by derived classes
    public abstract double GetPace(); // to be implemented by derived classes

    public virtual string GetSummary()
    {
        return $"{date:dd MMM yyyy} {GetType().Name} ({duration} min): Distance: {GetDistance():F1}, Speed: {GetSpeed():F1}, Pace: {GetPace():F1}";
    }
}

// Derived class for Running
public class Running : Activity
{
    private double distance; // in miles

    public Running(DateTime date, int duration, double distance) : base(date, duration)
    {
        this.distance = distance;
    }

    public override double GetDistance() => distance;

    public override double GetSpeed() => (distance / Duration) * 60; // mph

    public override double GetPace() => Duration / distance; // min per mile
}

// Derived class for Cycling
public class Cycling : Activity
{
    private double distance; // in miles

    public Cycling(DateTime date, int duration, double distance) : base(date, duration)
    {
        this.distance = distance;
    }

    public override double GetDistance() => distance;

    public override double GetSpeed() => (distance / Duration) * 60; // mph

    public override double GetPace() => Duration / distance; // min per mile
}

// Derived class for Swimming
public class Swimming : Activity
{
    private int laps;

    public Swimming(DateTime date, int duration, int laps) : base(date, duration)
    {
        this.laps = laps;
    }

    public override double GetDistance() => laps * 50 / 1000.0 * 0.62; // Convert to miles

    public override double GetSpeed() => (GetDistance() / Duration) * 60; // mph

    public override double GetPace() => Duration / GetDistance(); // min per mile
}

// Main Program
public class Program
{
    public static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2022, 11, 3), 30, 3.0), // 30 min running 3.0 miles
            new Cycling(new DateTime(2022, 11, 4), 45, 10.0), // 45 min cycling 10.0 miles
            new Swimming(new DateTime(2022, 11, 5), 30, 20)   // 30 min swimming 20 laps
        };

        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
