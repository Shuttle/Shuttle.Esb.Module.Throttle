﻿using System;
using System.ComponentModel;
using System.Configuration;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Esb.Module.Throttle
{
	public class ThrottleSection : ConfigurationSection
	{
		[ConfigurationProperty("cpuUsagePercentage", IsRequired = false, DefaultValue = "65")]
		public int CpuUsagePercentage
        {
			get { return (int)this["cpuUsagePercentage"]; }
		}

		[ConfigurationProperty("abortCycleCount", IsRequired = false, DefaultValue = "5")]
		public int AbortCycleCount
        {
			get { return (int)this["abortCycleCount"]; }
		}

		[ConfigurationProperty("performanceCounterReadInterval", IsRequired = false, DefaultValue = "1000")]
		public int PerformanceCounterReadInterval
        {
			get { return (int)this["performanceCounterReadInterval"]; }
		}

	    [TypeConverter(typeof(StringDurationArrayConverter))]
	    [ConfigurationProperty("durationToSleepOnAbort", IsRequired = false, DefaultValue = null)]
	    public TimeSpan[] DurationToSleepOnAbort
        {
	        get { return (TimeSpan[])this["durationToSleepOnAbort"]; }
	    }


        public static IThrottleConfiguration Configuration()
		{
			var section = ConfigurationSectionProvider.Open<ThrottleSection>("shuttle", "throttle");
			var configuration = new ThrottleConfiguration();

			if (section != null)
			{
			    configuration.DurationToSleepOnAbort = section.DurationToSleepOnAbort ?? configuration.DurationToSleepOnAbort;
                configuration.AbortCycleCount = section.AbortCycleCount;
                configuration.CpuUsagePercentage = section.CpuUsagePercentage;
                configuration.PerformanceCounterReadInterval = section.PerformanceCounterReadInterval;
			}

			return configuration;
		}
	}
}