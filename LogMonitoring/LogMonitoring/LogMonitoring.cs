using LogMonitoring.Models;

namespace LogMonitoring;
public class LogMonitoringImplementation : ILogMonitoring
{
    List<Job> jobs = new();

    public Result MonitorLogs(string logFilePath)
    {
        Console.WriteLine($"Monitoring logs from file: {logFilePath}");
        //TODO: use result to return if log file exists or not
        return PopulateJobsFromFile(logFilePath, out jobs);
    }

    private static Result PopulateJobsFromFile(string logFilePath, out List<Job> jobs)
    {
        jobs = new();
        string[] lines = File.ReadAllLines(logFilePath);
        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            if (parts.Length < 4)
            {
                return Result.Failure;
            }
            Job job = new()
            {
                Pid = int.Parse(parts[3]),
                Description = parts[1],
            };

            Result result = ParseStartOrStop(parts[2], out bool isStart);
            if (result == Result.Failure)
            {
                return result;
            }
            DateTime parsedTime = DateTime.Parse(parts[0]);
            if (isStart)
                job.StartTime = parsedTime;
            else
                job.EndTime = parsedTime;

            jobs.Add(job);
        }
        return Result.Success;
    }

    private static Result ParseStartOrStop(string startOrStop, out bool isStart)
    {
        isStart = true;
        if (string.IsNullOrWhiteSpace(startOrStop))
        {
            return Result.Failure;
        }
        string trimmedStartOrStop = startOrStop.Trim();
        if (trimmedStartOrStop.Equals("START"))
        {
            isStart = true;
            return Result.Success;
        }
        else if (trimmedStartOrStop.Equals("STOP"))
        {
            isStart = false;
            return Result.Success;
        }
        else
        {
            return Result.Failure;
        }
    }

    //Test method to check if the jobs are populated correctly
    public void PrintFirstJob()
    {
        if (jobs.Count == 0)
        {
            Console.WriteLine("No jobs found.");
            return;
        }
        Job firstJob = jobs.First();
        Console.WriteLine($"First job: {firstJob.Pid}, {firstJob.Description}, {firstJob.StartTime}, {firstJob.EndTime}");
    }
}
