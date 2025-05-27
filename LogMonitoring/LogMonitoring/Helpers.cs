using LogMonitoring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogMonitoring;
public class Helpers
{
    public static Result ParseStartOrStop(string startOrStop, out bool isStart)
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
        else if (trimmedStartOrStop.Equals("END"))
        {
            isStart = false;
            return Result.Success;
        }
        else
        {
            return Result.Failure;
        }
    }
}
