using LogMonitoring;
using LogMonitoring.Models;

namespace LogMonitoringTests;

[TestClass]
public sealed class Test1
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
}
