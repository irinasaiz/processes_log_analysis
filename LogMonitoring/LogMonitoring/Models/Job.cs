using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogMonitoring.Models;
public record Job
{
    //TODO: add description to each field
    public int Pid { get; set; }
    public required string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
