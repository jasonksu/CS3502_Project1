using System;
using System.Threading;
using System.Diagnostics;
using System.IO;

class Program
{
    private static object lockObj = new object();
    private static int counter = 0;

    static void Main()
    {
        Console.WriteLine("Starting Multi-Threading and IPC Project...");
        RunMultiThreading();
        RunIPC();
    }

    static void RunMultiThreading()
    {
        Console.WriteLine("\n[Multi-Threading Demonstration]");
        Thread t1 = new Thread(UpdateCounter);
        Thread t2 = new Thread(UpdateCounter);

        t1.Start();
        t2.Start();

        t1.Join();
        t2.Join();

        Console.WriteLine("Final Counter Value: " + counter);
    }

    static void UpdateCounter()
    {
        for (int i = 0; i < 5; i++)
        {
            lock (lockObj)
            {
                counter++;
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} updated counter to {counter}");
                Thread.Sleep(100);
            }
        }
    }

    static void RunIPC()
    {
        Console.WriteLine("\n[Inter-Process Communication Demonstration]");

        Process childProc = new Process();
        childProc.StartInfo.FileName = "dotnet";
        childProc.StartInfo.Arguments = "../ChildProcessProject/bin/Debug/net8.0/ChildProcessProject.dll";
        childProc.StartInfo.UseShellExecute = false;
        childProc.StartInfo.RedirectStandardInput = true;
        childProc.StartInfo.RedirectStandardOutput = true;

        childProc.Start();

        StreamWriter writer = childProc.StandardInput;
        StreamReader reader = childProc.StandardOutput;

        writer.WriteLine("Hello from parent process!");
        writer.Flush();

        string? response = reader.ReadLine();
        Console.WriteLine("Child process responded: " + response);

        childProc.WaitForExit();
    }
}
