using Shuttle.Core.Container;
using Shuttle.Core.Contract;

namespace Shuttle.Esb.Module.Throttle
{
    public class Bootstrap :
        IComponentRegistryBootstrap,
        IComponentResolverBootstrap
    {
        private static bool _registryBootstrapCalled;
        private static bool _resolverBootstrapCalled;

        public void Register(IComponentRegistry registry)
        {
            Guard.AgainstNull(registry, nameof(registry));

            if (_registryBootstrapCalled)
            {
                return;
            }

            _registryBootstrapCalled = true;

            if (!registry.IsRegistered<IThrottleConfiguration>())
            {
                registry.AttemptRegisterInstance(ThrottleSection.Configuration());
            }

            registry.AttemptRegister<IThrottlePolicy, ThrottlePolicy>();
            registry.AttemptRegister<ThrottleModule>();
        }

        public void Resolve(IComponentResolver resolver)
        {
            Guard.AgainstNull(resolver, nameof(resolver));

            if (_resolverBootstrapCalled)
            {
                return;
            }

            resolver.Resolve<ThrottleModule>();

            _resolverBootstrapCalled = true;
        }
    }
}