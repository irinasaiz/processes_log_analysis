using LogMonitoring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogMonitoring;
public interface ILogMonitoring
{
    Result MonitorLogs(string logFilePath);
}
