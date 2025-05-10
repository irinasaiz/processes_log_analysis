using LogMonitoring;
using LogMonitoring.Models;

namespace LogMonitoringTests;

[TestClass]
public sealed class LogMonitorsTests
{
    [TestMethod]
    public void TestMethod_ReturnFailure_WhenLogFileInexitent()
    {
        // Arrange
        var logMonitoring = new LogMonitoringImplementation();
        string invalidFilePath = "invalid_path.log";

        // Act
        var result = logMonitoring.MonitorLogs(invalidFilePath);

        // Assert
        Assert.AreEqual(Result.Failure, result);
    }

    [TestMethod]
    public void TestMethod_ReturnSuccess_WhenLogFileExists()
    {
        // Arrange
        var logMonitoring = new LogMonitoringImplementation();
        string validFilePath = "valid_logs.log";
        File.WriteAllLines(validFilePath, new[]
        {
                "11:35:23,scheduled task 032, START,37980\r\n" +
                "11:35:56,scheduled task 032, END,37980\r\n" +
                "11:36:11,scheduled task 796, START,57672\r\n" +
                "11:36:18,scheduled task 796, END,57672"
            });

        // Act
        var result = logMonitoring.MonitorLogs(validFilePath);

        // Assert
        Assert.AreEqual(Result.Success, result);
    }

    [TestMethod]
    public void TestMethod_ReturnFailure_WhenLogFileIsMalformed()
    {
        // Arrange
        var logMonitoring = new LogMonitoringImplementation();
        string validFilePathMalformedContent = "valid_logs_malformed_content.log";
        File.WriteAllLines(validFilePathMalformedContent, new[]
        {
                "11:35:23,scheduled task 032, START37980\r\n" +
                "11:35:56,scheduled task 032, END,37980\r\n" +
                "11:36:11,scheduled task 796, START,57672\r\n" +
                "11:36:18,scheduled task 796, END,57672"
            });

        // Act
        var result = logMonitoring.MonitorLogs(validFilePathMalformedContent);

        // Assert
        Assert.AreEqual(Result.Failure, result);
    }
}
