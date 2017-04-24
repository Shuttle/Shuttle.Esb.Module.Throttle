namespace Shuttle.Esb.Module.Throttle
{
    public interface IThrottlePolicy
    {
        bool ShouldAbort();
    }
}