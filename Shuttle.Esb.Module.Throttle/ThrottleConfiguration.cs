namespace Shuttle.Esb.Module.Throttle
{
	public class ThrottleConfiguration : IThrottleConfiguration
	{
		public string ActiveFromTime { get; set; }
		public string ActiveToTime { get; set; }

		public Throttle CreateThrottle()
		{
			return new Throttle(ActiveFromTime, ActiveToTime);
		}
	}
}