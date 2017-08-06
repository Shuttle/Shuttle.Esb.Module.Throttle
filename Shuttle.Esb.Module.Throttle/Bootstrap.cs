using Shuttle.Core.Infrastructure;

namespace Shuttle.Esb.Module.Throttle
{
    public class Bootstrap :
        IComponentRegistryBootstrap,
        IComponentResolverBootstrap
    {
        private static bool _registered;
        private static bool _registryBootstrapCalled;
        private static bool _resolverBootstrapCalled;

        public void Register(IComponentRegistry registry)
        {
            Guard.AgainstNull(registry, "registry");

            if (_registryBootstrapCalled)
            {
                return;
            }

            _registryBootstrapCalled = true;

            if (!registry.IsRegistered<IThrottleConfiguration>())
            {
                registry.AttemptRegister(ThrottleSection.Configuration());
            }

            registry.AttemptRegister<IThrottlePolicy, ThrottlePolicy>();
            registry.AttemptRegister<ThrottleModule>();

            _registered = true;
        }

        public void Resolve(IComponentResolver resolver)
        {
            Guard.AgainstNull(resolver, "resolver");

            if (_resolverBootstrapCalled || !_registered)
            {
                return;
            }

            resolver.Resolve<ThrottleModule>();

            _resolverBootstrapCalled = true;
        }
    }
}