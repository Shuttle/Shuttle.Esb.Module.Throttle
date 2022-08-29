using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shuttle.Core.Contract;
using Shuttle.Core.Pipelines;

namespace Shuttle.Esb.Module.Throttle
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddThrottleModule(this IServiceCollection services,
            Action<ThrottleBuilder> builder = null)
        {
            Guard.AgainstNull(services, nameof(services));

            var throttleBuilder = new ThrottleBuilder(services);

            builder?.Invoke(throttleBuilder);

            services.TryAddSingleton<ThrottleModule, ThrottleModule>();
            services.TryAddSingleton<ThrottleObserver, ThrottleObserver>();
            services.TryAddSingleton<IThrottlePolicy, ThrottlePolicy>();

            services.AddOptions<ThrottleOptions>().Configure(options =>
            {
                options.AbortCycleCount = throttleBuilder.Options.AbortCycleCount;
                options.CpuUsagePercentage = throttleBuilder.Options.CpuUsagePercentage;
                options.DurationToSleepOnAbort = throttleBuilder.Options.DurationToSleepOnAbort;
            });

            services.AddPipelineModule<ThrottleModule>();

            return services;
        }

    }
}