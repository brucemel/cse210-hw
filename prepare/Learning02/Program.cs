using System;

class Program
{
    static void Main(string[] args)
    {
        // Create job instances
        Job job1 = new Job("Software Engineer", "Microsoft", 2019, 2022);
        Job job2 = new Job("Manager", "Apple", 2022, 2023);

        // Create a resume instance
        Resume myResume = new Resume("Allison Rose");
        myResume.AddJob(job1);
        myResume.AddJob(job2);

        // Display the resume
        myResume.Display();
    }
}
