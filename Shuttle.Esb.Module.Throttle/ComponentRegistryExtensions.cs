using Shuttle.Core.Container;
using Shuttle.Core.Contract;

namespace Shuttle.Esb.Module.Throttle
{
    public static class ComponentRegistryExtensions
    {
        public static void RegisterThrottle(this IComponentRegistry registry)
        {
            Guard.AgainstNull(registry, nameof(registry));

            if (!registry.IsRegistered<IThrottleConfiguration>())
            {
                registry.AttemptRegisterInstance(ThrottleSection.Configuration());
            }

            registry.AttemptRegister<IThrottlePolicy, ThrottlePolicy>();
            registry.AttemptRegister<ThrottleModule>();
        }
    }
}