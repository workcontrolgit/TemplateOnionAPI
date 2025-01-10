namespace $safeprojectname$.Interfaces
{
    // Defines an interface for a service that provides access to the current UTC date and time.
    public interface IDateTimeService
    {
        // Gets the current UTC date and time.
        DateTime NowUtc { get; }
    }
}