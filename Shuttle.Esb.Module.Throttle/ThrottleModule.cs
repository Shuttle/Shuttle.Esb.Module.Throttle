using System;
using System.Collections.Generic;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Esb.Module.Throttle
{
    public class ThrottleModule : IDisposable, IThreadState
    {
        private readonly IThrottleConfiguration _configuration;
        private readonly IThrottlePolicy _policy;
        private readonly string _startupPipelineName = typeof(StartupPipeline).FullName;
        private readonly string _transportMessagePipeline = typeof(TransportMessagePipeline).FullName;
        private volatile bool _active;

        public ThrottleModule(IPipelineFactory pipelineFactory, IThrottleConfiguration configuration, IThrottlePolicy policy)
        {
            Guard.AgainstNull(pipelineFactory, "pipelineFactory");
            Guard.AgainstNull(configuration, "configuration");
            Guard.AgainstNull(policy, "policy");

            pipelineFactory.PipelineCreated += PipelineCreated;

            _configuration = configuration;
            _policy = policy;
        }

        public void Dispose()
        {
            _active = false;
        }

        public bool Active
        {
            get { return _active; }
        }

        private void PipelineCreated(object sender, PipelineEventArgs e)
        {
            var pipelineFullName = e.Pipeline.GetType().FullName;

            if (pipelineFullName.Equals(_startupPipelineName, StringComparison.InvariantCultureIgnoreCase)
                ||
                pipelineFullName.Equals(_transportMessagePipeline, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            e.Pipeline.RegisterObserver(new ThrottleObserver(this, _configuration, _policy));
        }
    }
}