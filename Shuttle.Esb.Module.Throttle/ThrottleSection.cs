using System.Configuration;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Esb.Module.Throttle
{
	public class ThrottleSection : ConfigurationSection
	{
		[ConfigurationProperty("from", IsRequired = false, DefaultValue = "*")]
		public string From
		{
			get { return (string)this["from"]; }
		}

		[ConfigurationProperty("to", IsRequired = false, DefaultValue = "*")]
		public string To
		{
			get { return (string)this["to"]; }
		}

		public static IThrottleConfiguration Configuration()
		{
			var section = ConfigurationSectionProvider.Open<ThrottleSection>("shuttle", "Throttle");
			var configuration = new ThrottleConfiguration();

			if (section != null)
			{
				configuration.ActiveFromTime = section.From;
				configuration.ActiveToTime = section.To;
			}

			return configuration;
		}
	}
}