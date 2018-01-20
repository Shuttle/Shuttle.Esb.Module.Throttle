using System;

namespace Shuttle.Esb.Module.Throttle
{
    public class ThrottleConfiguration : IThrottleConfiguration
    {
        private readonly TimeSpan[] _defaultDurationToSleepOnAbort;
        private TimeSpan[] _durationToSleepOnAbort;

        public ThrottleConfiguration()
        {
            CpuUsagePercentage = 65;
            AbortCycleCount = 5;
            _defaultDurationToSleepOnAbort = DurationToSleepOnAbort = new[] {TimeSpan.FromSeconds(1)};
            PerformanceCounterReadInterval = 1000;
        }

        public int CpuUsagePercentage { get; set; }
        public int AbortCycleCount { get; set; }

        public TimeSpan[] DurationToSleepOnAbort
        {
            get => _durationToSleepOnAbort ?? _defaultDurationToSleepOnAbort;
            set => _durationToSleepOnAbort = value;
        }

        public int PerformanceCounterReadInterval { get; set; }
    }
}