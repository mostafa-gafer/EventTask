using EventTask.Events.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;

namespace EventTask.Events.Interfaces;

public interface IEventRepository : IRepository<Event, Guid>
{
}
