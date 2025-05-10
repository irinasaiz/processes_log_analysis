using LogMonitoring.Models;

namespace LogMonitoring;
public class LogMonitoringImplementation : ILogMonitoring
{
    List<Job> jobs = new();

    public void MonitorLogs(string logFilePath)
    {
        Console.WriteLine($"Monitoring logs from file: {logFilePath}");
        //TODO: use result to return if log file exists or not
        jobs = PopulateJobsFromFile(logFilePath);
    }

    private static List<Job> PopulateJobsFromFile(string logFilePath)
    {
        List<Job> jobs = new();
        string[] lines = File.ReadAllLines(logFilePath);
        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            if (parts.Length < 4)
            {
                //TODO: return result fail
                continue;
            }
            Job job = new Job
            {
                Pid = int.Parse(parts[3]),
                Description = parts[1],
            };

            bool isStart = ParseStartOrStop(parts[2]);
            DateTime parsedTime = DateTime.Parse(parts[0]);
            if (isStart)
                job.StartTime = parsedTime;
            else
                job.EndTime = parsedTime;

            jobs.Add(job);
        }
        return jobs;
    }

    private static bool ParseStartOrStop(string startOrStop)
    {
        string trimmedstartOrStop = startOrStop.Trim();
        if (trimmedstartOrStop.Equals("start", StringComparison.OrdinalIgnoreCase))
            return true;
        else return false;
        //else return result fail
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
