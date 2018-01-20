using System;
using Shuttle.Core.Contract;
using Shuttle.Core.Pipelines;
using Shuttle.Core.Threading;

namespace Shuttle.Esb.Module.Throttle
{
    public class ThrottleObserver : IPipelineObserver<OnPipelineStarting>
    {
        private readonly IThrottlePolicy _policy;
        private readonly IThreadState _state;
        private readonly IThrottleConfiguration _configuration;
        private int _abortCount;

        public ThrottleObserver(IThreadState state, IThrottleConfiguration configuration, IThrottlePolicy policy)
        {
            Guard.AgainstNull(state, nameof(state));
            Guard.AgainstNull(configuration, nameof(configuration));
            Guard.AgainstNull(policy, nameof(policy));

            _state = state;
            _configuration = configuration;
            _policy = policy;
        }

        public void Execute(OnPipelineStarting pipelineEvent)
        {
            if (!_policy.ShouldAbort())
            {
                _abortCount = 0;
                return;
            }

            pipelineEvent.Pipeline.Abort();
            int sleep = 1000;

            try
            {
                sleep = _configuration.DurationToSleepOnAbort[_abortCount].Milliseconds;
            }
            catch
            {
            }

            ThreadSleep.While(sleep, _state);

            _abortCount += _abortCount + 1 < _configuration.DurationToSleepOnAbort.Length ? 1 : 0;
        }
    }
}