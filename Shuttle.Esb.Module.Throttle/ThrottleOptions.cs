using System;
using System.Collections.Generic;

namespace Shuttle.Esb.Module.Throttle
{
    public class ThrottleOptions
    {
        public const string SectionName = "Shuttle:ServiceBus:Modules:Throttle";

        public ThrottleOptions()
        {
            CpuUsagePercentage = 65;
            AbortCycleCount = 5;
        }

        public int CpuUsagePercentage { get; set; }
        public int AbortCycleCount { get; set; }
        public List<TimeSpan> DurationToSleepOnAbort { get; set; } = new List<TimeSpan> { TimeSpan.FromSeconds(1) };
    }
}