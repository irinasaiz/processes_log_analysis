using LogMonitoring;

namespace LogMonitoringApp;
public class Program
{
    public static void Main()
    {
        string logFilePath = "C:\\Learning\\processes_log_analysis\\LogMonitoring\\LogMonitoring\\logs.log";
        LogMonitoringImplementation logMonitoring = new LogMonitoringImplementation();
        logMonitoring.MonitorLogs(logFilePath);

        // Test method to check if the jobs are populated correctly
        logMonitoring.PrintFirstJob();
    }
}