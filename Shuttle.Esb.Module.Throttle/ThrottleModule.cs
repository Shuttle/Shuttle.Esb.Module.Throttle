using System;
using Shuttle.Core.Contract;
using Shuttle.Core.Pipelines;

namespace Shuttle.Esb.Module.Throttle
{
    public class ThrottleModule : IDisposable
    {
        private readonly IThrottleConfiguration _configuration;
        private readonly IThrottlePolicy _policy;
        private readonly string _shutdownPipelineName = typeof(ShutdownPipeline).FullName;
        private readonly string _startupPipelineName = typeof(StartupPipeline).FullName;
        private readonly string _transportMessagePipeline = typeof(TransportMessagePipeline).FullName;
        private volatile bool _active;

        public ThrottleModule(IPipelineFactory pipelineFactory, IThrottleConfiguration configuration,
            IThrottlePolicy policy)
        {
            Guard.AgainstNull(pipelineFactory, nameof(pipelineFactory));
            Guard.AgainstNull(configuration, nameof(configuration));
            Guard.AgainstNull(policy, nameof(policy));

            pipelineFactory.PipelineCreated += PipelineCreated;

            _configuration = configuration;
            _policy = policy;
        }

        public void Dispose()
        {
            _active = false;
        }

        private void PipelineCreated(object sender, PipelineEventArgs e)
        {
            var pipelineName = e.Pipeline.GetType().FullName ?? string.Empty;

            if (pipelineName.Equals(_startupPipelineName, StringComparison.InvariantCultureIgnoreCase)
                ||
                pipelineName.Equals(_transportMessagePipeline, StringComparison.InvariantCultureIgnoreCase)
                ||
                pipelineName.Equals(_shutdownPipelineName, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            e.Pipeline.RegisterObserver(new ThrottleObserver(e.Pipeline.State.GetCancellationToken(), _configuration, _policy));
        }
    }
}