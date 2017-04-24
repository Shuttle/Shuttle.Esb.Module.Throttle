using System;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Esb.Module.Throttle
{
    public class ThrottleModule : IDisposable, IThreadState
    {
	    private readonly Throttle _Throttle;
        private readonly string _startupPipelineName = typeof (StartupPipeline).FullName;
        private volatile bool _active;

        public ThrottleModule(IPipelineFactory pipelineFactory, IThrottleConfiguration ThrottleConfiguration )
        {
	        Guard.AgainstNull(pipelineFactory, "pipelineFactory");
	        Guard.AgainstNull(ThrottleConfiguration, "ThrottleConfiguration");

			_Throttle = ThrottleConfiguration.CreateThrottle();

			pipelineFactory.PipelineCreated += PipelineCreated;
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
            if (e.Pipeline.GetType().FullName.Equals(_startupPipelineName, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            e.Pipeline.RegisterObserver(new ThrottleObserver(this, _Throttle));
        }
    }
}