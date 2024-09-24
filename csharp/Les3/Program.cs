// See https://aka.ms/new-console-template for more information
using Les3;
Project projectReis = new Project("Reis", "op vakantie naar SPanje");
Console.WriteLine(projectReis.ToString());
projectReis.EditDescription("Reis","Op vakantie naar Turkije");
Console.WriteLine(projectReis.ToString());

MyQueue<int> queue = new MyQueue<int>();

queue.Add(4);
queue.Add(5);
queue.Add(6);

Console.WriteLine(queue.ToString());
queue.Clear();


MyQueue<Project> project1 = new MyQueue<Project>();

// Adding projects to the queue
project1.Add(new Project("Project1", "Work on task 1"));
project1.Add(new Project("Project2", "Work on task 2"));
project1.Add(new Project("Project3", "Work on task 3"));

Console.WriteLine("Queue of Projects:");
Console.WriteLine(project1.ToString());

Console.WriteLine("\nNext project out of the queue:");
Project nextProject = project1.NextOut();
Console.WriteLine(nextProject.ToString());

Console.WriteLine("\nRemaining queue of projects:");
Console.WriteLine(project1.ToString());