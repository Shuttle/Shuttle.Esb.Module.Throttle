using Shuttle.Core.Infrastructure;

namespace Shuttle.Esb.Module.Throttle
{
    public class CpuThrottlePolicy : IThrottlePolicy
    {
        private readonly IThrottleConfiguration _configuration;
        private readonly int _abortCount;

        public CpuThrottlePolicy(IThrottleConfiguration configuration)
        {
            Guard.AgainstNull(configuration, "configuration");

            _configuration = configuration;
        }

        public bool ShouldAbort()
        {
            throw new System.NotImplementedException();
        }
    }
}