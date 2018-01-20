using System;
using System.ComponentModel;
using System.Configuration;
using Shuttle.Core.Configuration;
using Shuttle.Core.Logging;
using Shuttle.Core.TimeSpanTypeConverters;

namespace Shuttle.Esb.Module.Throttle
{
    public class ThrottleSection : ConfigurationSection
    {
        [ConfigurationProperty("cpuUsagePercentage", IsRequired = false, DefaultValue = "65")]
        public int CpuUsagePercentage => (int) this["cpuUsagePercentage"];

        [ConfigurationProperty("abortCycleCount", IsRequired = false, DefaultValue = "5")]
        public int AbortCycleCount => (int) this["abortCycleCount"];

        [ConfigurationProperty("performanceCounterReadInterval", IsRequired = false, DefaultValue = "1000")]
        public int PerformanceCounterReadInterval => (int) this["performanceCounterReadInterval"];

        [TypeConverter(typeof(StringDurationArrayConverter))]
        [ConfigurationProperty("durationToSleepOnAbort", IsRequired = false, DefaultValue = null)]
        public TimeSpan[] DurationToSleepOnAbort => (TimeSpan[]) this["durationToSleepOnAbort"];

        public static IThrottleConfiguration Configuration()
        {
            var section = ConfigurationSectionProvider.Open<ThrottleSection>("shuttle", "throttle");

            var configuration = new ThrottleConfiguration();

            if (section != null)
            {
                configuration.DurationToSleepOnAbort =
                    section.DurationToSleepOnAbort ?? configuration.DurationToSleepOnAbort;
                configuration.AbortCycleCount = section.AbortCycleCount;
                configuration.CpuUsagePercentage = section.CpuUsagePercentage;
                configuration.PerformanceCounterReadInterval = section.PerformanceCounterReadInterval;
            }
            else
            {
                Log.Information(Resources.DefaultConfigurationApplied);
            }

            return configuration;
        }
    }
}