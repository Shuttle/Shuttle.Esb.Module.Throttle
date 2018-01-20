using System.Diagnostics;
using Shuttle.Core.Contract;

namespace Shuttle.Esb.Module.Throttle
{
    public class ThrottlePolicy : IThrottlePolicy
    {
        private readonly IThrottleConfiguration _configuration;
        private readonly PerformanceCounterValue _performanceCounterValue;
        private int _abortCount;

        public ThrottlePolicy(IThrottleConfiguration configuration)
        {
            Guard.AgainstNull(configuration, nameof(configuration));

            _configuration = configuration;
            _performanceCounterValue = new PerformanceCounterValue(new PerformanceCounter
            {
                CategoryName = "Processor",
                CounterName = "% Processor Time",
                InstanceName = "_Total"
            }, configuration.PerformanceCounterReadInterval);
        }

        public bool ShouldAbort()
        {
            if (_performanceCounterValue.NextValue() > _configuration.CpuUsagePercentage)
            {
                _abortCount++;

                if (_abortCount > _configuration.AbortCycleCount)
                {
                    _abortCount = 0;
                }

                return _abortCount > 0;
            }

            _abortCount = 0;

            return false;
        }
    }
}