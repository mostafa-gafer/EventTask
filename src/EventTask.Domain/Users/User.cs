using EventTask.EventRegistrations;
using EventTask.Events.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Identity;

namespace EventTask.Users;

public class User: IdentityUser
{

    private readonly List<Event> _events = new();

    public IReadOnlyCollection<Event> Events => _events.AsReadOnly();


    private readonly List<EventRegistration> _registrations = new();

    public IReadOnlyCollection<EventRegistration> Registrations => _registrations.AsReadOnly();
}
