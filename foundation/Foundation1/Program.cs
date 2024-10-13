using System;
using System.Collections.Generic;

public class Comment
{
    public string _name { get; }
    public string _text { get; }

    public Comment(string name, string text)
    {
        _name = name;
        _text = text;
    }
}

public class Video
{
    public string Title { get; }
    public string Author { get; }
    public int Length { get; }
    private List<Comment> _comments;

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        _comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return _comments.Count;
    }

    public List<Comment> RetrieveComments()
    {
        return _comments;
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Video> videoList = new List<Video>();

        // Creating video objects
        Video video1 = new Video("C# Basics", "Alice", 300);
        video1.AddComment(new Comment("Bob", "Great tutorial!"));
        video1.AddComment(new Comment("Charlie", "Very informative, thanks!"));
        video1.AddComment(new Comment("David", "Helpful examples!"));

        Video video2 = new Video("OOP Overview", "Eve", 240);
        video2.AddComment(new Comment("Frank", "Well done!"));
        video2.AddComment(new Comment("Grace", "Clear explanations!"));
        
        Video video3 = new Video("Advanced C# Techniques", "Hannah", 360);
        video3.AddComment(new Comment("Isaac", "Excellent examples!"));
        video3.AddComment(new Comment("Jasmine", "Loved the details!"));
        
        // Adding videos to the list
        videoList.Add(video1);
        videoList.Add(video2);
        videoList.Add(video3);

        // Displaying video details
        foreach (var video in videoList)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.Length} seconds");
            Console.WriteLine($"Number of Comments: {video.GetCommentCount()}");
            Console.WriteLine("Comments:");

            foreach (var comment in video.RetrieveComments())
            {
                Console.WriteLine($"- {comment._name}: {comment._text}");
            }
            Console.WriteLine();
        }
    }
}
