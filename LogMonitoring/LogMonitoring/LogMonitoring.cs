using LogMonitoring.Models;

namespace LogMonitoring;
public class LogMonitoringImplementation : ILogMonitoring
{
    List<Job> jobs = [];
    readonly string outputFile = "C:\\Learning\\processes_log_analysis\\LogMonitoring\\LogMonitoring\\JobDurationOutput.txt";

    public Result MonitorLogs(string logFilePath)
    {
        Console.WriteLine($"Monitoring logs from file: {logFilePath}");
        Result populateResult = PopulateJobsFromFile(logFilePath);
        if (populateResult == Result.Failure)
        {
            Console.WriteLine("Failed to populate jobs from file.");
            return Result.Failure;
        }

        ProcessJobsDuration();
        return Result.Success;
    }

    private void ProcessJobsDuration()
    {
        using StreamWriter logFile = new(outputFile);

        foreach (var job in jobs)
        {
            TimeSpan duration = job.EndTime - job.StartTime;

            if (duration > TimeSpan.FromMinutes(10))
            {
                logFile.WriteLine($"Error: Job with PID {job.Pid} took longer than 10 minutes. Duration: {duration}");
            }
            else if (duration > TimeSpan.FromMinutes(5))
            {
                logFile.WriteLine($"Warning: Job with PID {job.Pid} took longer than 5 minutes. Duration: {duration}");
            }
        }
    }

    private Result PopulateJobsFromFile(string logFilePath)
    {
        if (!File.Exists(logFilePath))
        {
            Console.WriteLine($"Log file not found: {logFilePath}");
            return Result.Failure;
        }

        string[] lines = File.ReadAllLines(logFilePath);
        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            if (parts.Length < 4)
            {
                return Result.Failure;
            }

            Result resultStartOrStop = Helpers.ParseStartOrStop(parts[2], out bool isStart);
            if (resultStartOrStop == Result.Failure)
            {
                return resultStartOrStop;
            }
            DateTime parsedTime = DateTime.Parse(parts[0]);

            int pid = int.Parse(parts[3]);
            Job existingJob = jobs.FirstOrDefault(job => job.Pid == pid);
            if (existingJob == null)
            {
                Job job = new()
                {
                    Pid = int.Parse(parts[3]),
                    Description = parts[1],
                };                
                if (isStart)
                    job.StartTime = parsedTime;
                else
                    job.EndTime = parsedTime;

                jobs.Add(job);
            }
            else
            {
                if (isStart)
                    existingJob.StartTime = parsedTime;
                else
                    existingJob.EndTime = parsedTime;
            }
        }
        return Result.Success;
    }    
}
