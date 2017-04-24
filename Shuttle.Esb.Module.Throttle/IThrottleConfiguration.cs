using System;

namespace Shuttle.Esb.Module.Throttle
{
	public interface IThrottleConfiguration
	{
		int CpuUsagePercentage { get; }
		int AbortCount { get; }
        TimeSpan[] DurationToSleepOnAbort { get; }
        int PerformanceCounterReadInterval { get; }
    }
}