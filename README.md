# Shuttle.Esb.Module.Throttle

The Throttle module for Shuttle.Esb aborts pipeline processing when the CPU usage exceeds given percentage.

The module will attach the `ThrottleObserver` to the `OnPipelineStarting` event of all pipelines except the `StartupPipeline` and abort the pipeline if the SPU usage is exceeds the given percentage.

```xml
<configuration>
	<configSections>
		<section name="throttle" type="Shuttle.Esb.Module.Throttle.ThrottleSection, Shuttle.Esb.Module.Throttle"/>
	</configSections>

  <throttle 
	cpuUsagePercentage="65"
	abortCycleCount="5"
	performanceCounterReadInterval="1000"
	durationToSleepOnAbort="1s" />
</configuration>
```

The default value of "*" ignores the value.  If both `from` and `to` are specified as "*" no pipeline will be aborted.

The module will register/resolve itself using [Shuttle.Core container bootstrapping](http://shuttle.github.io/shuttle-core/overview-container/#bootstrapping).