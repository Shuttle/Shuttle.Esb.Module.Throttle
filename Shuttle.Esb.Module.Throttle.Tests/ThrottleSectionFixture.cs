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
			var section = ConfigurationSectionProvider.OpenFile<ThrottleSection>("shuttle", "Throttle",
				Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format(@"config-files\{0}", file)));

			Assert.IsNotNull(section);
			Assert.AreEqual("8:00", section.From);
			Assert.AreEqual("23:00", section.To);
		}
	}
}