using System;
using Microsoft.Extensions.Options;
using Shuttle.Core.Contract;
using Shuttle.Core.Pipelines;

namespace Shuttle.Esb.Module.Throttle
{
    public class ThrottleModule
    {
        private readonly IThrottlePolicy _policy;
        private readonly Type _inboxMessagePipelineType = typeof(InboxMessagePipeline);
        private readonly ThrottleOptions _throttleOptions;

        public ThrottleModule(IOptions<ThrottleOptions> throttleOptions, IPipelineFactory pipelineFactory,
            IThrottlePolicy policy)
        {
            Guard.AgainstNull(throttleOptions, nameof(throttleOptions));
            Guard.AgainstNull(throttleOptions.Value, nameof(throttleOptions.Value));
            Guard.AgainstNull(pipelineFactory, nameof(pipelineFactory));
            Guard.AgainstNull(policy, nameof(policy));

            pipelineFactory.PipelineCreated += PipelineCreated;

            _throttleOptions = throttleOptions.Value;
            _policy = policy;
        }

        private void PipelineCreated(object sender, PipelineEventArgs e)
        {
            var pipelineType = e.Pipeline.GetType();

            if (pipelineType != _inboxMessagePipelineType)
            {
                return;
            }

            e.Pipeline.RegisterObserver(new ThrottleObserver(_throttleOptions, _policy, e.Pipeline.State.GetCancellationToken()));
        }
    }
}