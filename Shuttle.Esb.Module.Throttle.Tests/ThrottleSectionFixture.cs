using System;
using System.IO;
using NUnit.Framework;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Esb.Module.Throttle.Tests
{
	[TestFixture]
	public class ThrottleSectionFixture
	{
		[Test]
		[TestCase("Throttle.config")]
		[TestCase("Throttle-Grouped.config")]
		public void Should_be_able_to_load_the_configuration(string file)
		{
			var section = ConfigurationSectionProvider.OpenFile<ThrottleSection>("shuttle", "throttle",
				Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format(@"config-files\{0}", file)));

			Assert.IsNotNull(section);
			Assert.AreEqual(35, section.CpuUsagePercentage);
			Assert.AreEqual(2500, section.PerformanceCounterReadInterval);
			Assert.AreEqual(7, section.AbortCycleCount);
			Assert.AreEqual(TimeSpan.FromSeconds(2), section.DurationToSleepOnAbort[0]);
			Assert.AreEqual(TimeSpan.FromSeconds(2), section.DurationToSleepOnAbort[1]);
			Assert.AreEqual(TimeSpan.FromSeconds(3), section.DurationToSleepOnAbort[2]);
		}
	}
}