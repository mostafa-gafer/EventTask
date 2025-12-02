using EventTask.Events.Entities;
using EventTask.Users;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace EventTask.EventRegistrations;

public class EventRegistration : AuditedAggregateRoot<Guid>
{
    public Guid EventId { get; private set; }

    public Event Event { get; private set; }

    public Guid UserId { get; private set; }

    public User User { get; private set; }

    public DateTime RegisteredAt { get; private set; }

    protected EventRegistration() { }

    public EventRegistration(Guid id, Guid eventId, Guid userId) : base(id)
    {
        EventId = eventId;
        UserId = userId;
        RegisteredAt = DateTime.UtcNow;
    }
}
