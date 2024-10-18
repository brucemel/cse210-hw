using System;
using System.Collections.Generic;

public class Assignment
{
    private string _studentName;
    private string _assignmentTopic;

    public Assignment(string studentName, string assignmentTopic)
    {
        _studentName = studentName;
        _assignmentTopic = assignmentTopic;
    }

    public string GetSummary()
    {
        return $"{_studentName} - {_assignmentTopic}";
    }

    protected string GetStudentName()
    {
        return _studentName;
    }
}

public class MathAssignment : Assignment
{
    private string _sectionNumber;
    private string _problemSet;

    public MathAssignment(string studentName, string assignmentTopic, string sectionNumber, string problemSet)
        : base(studentName, assignmentTopic)
    {
        _sectionNumber = sectionNumber;
        _problemSet = problemSet;
    }

    public string GetHomeworkList()
    {
        return $"Section {_sectionNumber} Problems {_problemSet}";
    }
}

public class WritingAssignment : Assignment
{
    private string _assignmentTitle;

    public WritingAssignment(string studentName, string assignmentTopic, string assignmentTitle)
        : base(studentName, assignmentTopic)
    {
        _assignmentTitle = assignmentTitle;
    }

    public string GetWritingInformation()
    {
        return $"{_assignmentTitle} by {GetStudentName()}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Assignment> assignmentsList = new List<Assignment>();

        // Adding different types of assignments to the list
        assignmentsList.Add(new MathAssignment("Roberto Rodriguez", "Fractions", "7.3", "8-19"));
        assignmentsList.Add(new WritingAssignment("Mary Waters", "European History", "The Causes of World War II"));
        assignmentsList.Add(new Assignment("Samuel Bennett", "Multiplication")); // Base class

        // Displaying summaries of all assignments
        foreach (var assignment in assignmentsList)
        {
            Console.WriteLine(assignment.GetSummary());

            // Check if the assignment is of type MathAssignment
            if (assignment is MathAssignment mathAssignment)
            {
                Console.WriteLine(mathAssignment.GetHomeworkList());
            }
            // Check if the assignment is of type WritingAssignment
            else if (assignment is WritingAssignment writingAssignment)
            {
                Console.WriteLine(writingAssignment.GetWritingInformation());
            }
        }
    }
}
