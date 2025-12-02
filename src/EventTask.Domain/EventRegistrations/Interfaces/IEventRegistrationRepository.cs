using EventTask.Events.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;

namespace EventTask.EventRegistrations.Interfaces;

public interface IEventRegistrationRepository : IRepository<EventRegistration, Guid>
{
}
