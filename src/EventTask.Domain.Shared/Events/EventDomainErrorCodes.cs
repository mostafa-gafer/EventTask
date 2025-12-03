namespace EventTask.Events;

public class EventDomainErrorCodes
{
    public const string InvalidDateRange = "EventRegistration:InvalidDateRange";
    public const string InvalidCapacity = "EventRegistration:InvalidCapacity";
    public const string EventNotActive = "EventRegistration:EventNotActive";
    public const string CapacityFull = "EventRegistration:CapacityFull";
    public const string AlreadyRegistered = "EventRegistration:AlreadyRegistered";
    public const string RegistrationNotFound = "EventRegistration:RegistrationNotFound";
    public const string CannotCancelNow = "EventRegistration:CannotCancelNow";
    public const string UnauthorizedAccess = "EventRegistration:UnauthorizedAccess";
}
