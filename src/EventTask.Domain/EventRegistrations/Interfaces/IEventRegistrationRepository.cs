using EventTask.Events.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EventTask.EventRegistrations.Interfaces;

public interface IEventRegistrationRepository : IRepository<EventRegistration, Guid>
{
    Task<EventRegistration> FindByEventAndUserAsync(Guid eventId, Guid userId);
    Task<int> GetActiveRegistrationsCountAsync(Guid eventId);
    Task<bool> IsUserRegisteredAsync(Guid eventId, Guid userId);
}
