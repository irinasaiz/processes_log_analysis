using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogMonitoring;
public interface ILogMonitoring
{
    void MonitorLogs(string logFilePath);
}
