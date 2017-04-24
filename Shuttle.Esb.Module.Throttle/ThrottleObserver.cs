using Shuttle.Core.Infrastructure;

namespace Shuttle.Esb.Module.Throttle
{
	internal class ThrottleObserver : IPipelineObserver<OnPipelineStarting>
	{
		private readonly IThreadState _state;

		private readonly IThrottlePolicy _configuration;

		public ThrottleObserver(IThreadState state, IThrottlePolicy configuration)
		{
			Guard.AgainstNull(state, "state");

			_state = state;
			_configuration = configuration;
		}

		public void Execute(OnPipelineStarting pipelineEvent)
		{
			const int SLEEP = 15000;

			if (_configuration.Active())
			{
				return;
			}

			pipelineEvent.Pipeline.Abort();

			ThreadSleep.While(SLEEP, _state);
		}
	}
}