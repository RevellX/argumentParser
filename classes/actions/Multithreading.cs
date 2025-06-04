namespace argumentParser.Classes.Actions;

class Multithreading : IActionable
{
    public void Run()
    {
        // Thread thread1 = new Thread(() =>
        // {
        //     for (int i = 0; i < 5; i++)
        //     {
        //         Console.WriteLine($"Thread 1: {i}");
        //         Thread.Sleep(500);
        //     }
        // });

        // Thread thread2 = new Thread(() =>
        // {
        //     for (int i = 0; i < 5; i++)
        //     {
        //         Console.WriteLine($"Thread 2: {i}");
        //         Thread.Sleep(700);
        //     }
        // });

        // thread1.Start();
        // thread2.Start();

        // thread1.Join();
        // thread2.Join();

        // Console.WriteLine("Both threads have finished.");

        // Loop that executes exactly X times per second 
        var process = System.Diagnostics.Process.GetCurrentProcess();

        const int TARGET_FPS = 20;
        int minElapsedTime = 1000 / TARGET_FPS; // Minimum time per frame in milliseconds
        double startCpuTime;
        int elapsed;
        while (true)
        {
            // Get the current CPU time at the start of the loop
            startCpuTime = process.TotalProcessorTime.TotalMilliseconds;

            // Actual work
            // ...

            // Calculate the elapsed time since the start of the loop
            elapsed = (int)(process.TotalProcessorTime.TotalMilliseconds - startCpuTime);
            // If elapsed time is less than the minimum required time, sleep to maintain the target FPS
            if (elapsed < minElapsedTime)
            {
                Thread.Sleep(minElapsedTime - elapsed); // Sleep to maintain the target FPS
            }
            else
            {
                Logger.DisplayWarning("Script is taking too long to execute. Consider optimizing the code or increasing the target FPS.");
            }
        }
    }

    public string GetDescription()
    {
        throw new NotImplementedException();
    }

    public string GetName()
    {
        throw new NotImplementedException();
    }
}