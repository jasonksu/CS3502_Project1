using System;

class ChildProcess
{
    static void Main()
    {
        string? input = Console.ReadLine();
        if (!string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Received message from parent: " + input);
        }
        else
        {
            Console.WriteLine("No input received from parent.");
        }
    }
}

