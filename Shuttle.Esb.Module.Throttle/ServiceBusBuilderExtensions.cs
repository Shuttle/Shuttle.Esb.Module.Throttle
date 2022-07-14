using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shuttle.Core.Contract;

namespace Shuttle.Esb.Module.Throttle
{
    public static class ServiceBusBuilderExtensions
    {
        public static ServiceBusBuilder AddThrottleModule(this ServiceBusBuilder serviceBusBuilder,
            Action<ThrottleBuilder> builder = null)
        {
            Guard.AgainstNull(serviceBusBuilder, nameof(serviceBusBuilder));

            var throttleBuilder = new ThrottleBuilder(serviceBusBuilder.Services);

            builder?.Invoke(throttleBuilder);

            serviceBusBuilder.Services.TryAddSingleton<ThrottleModule, ThrottleModule>();
            serviceBusBuilder.Services.TryAddSingleton<ThrottleObserver, ThrottleObserver>();
            serviceBusBuilder.Services.TryAddSingleton<IThrottlePolicy, ThrottlePolicy>();

            serviceBusBuilder.Services.AddOptions<ThrottleOptions>().Configure(options =>
            {
                options.AbortCycleCount = throttleBuilder.Options.AbortCycleCount;
                options.CpuUsagePercentage = throttleBuilder.Options.CpuUsagePercentage;
                options.DurationToSleepOnAbort = throttleBuilder.Options.DurationToSleepOnAbort;
            });

            serviceBusBuilder.AddModule<ThrottleModule>();

            return serviceBusBuilder;
        }

    }
}