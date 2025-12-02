using EventTask.EventRegistrations;
using EventTask.EventRegistrations.Interfaces;
using EventTask.Events.Entities;
using EventTask.Events.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.EntityFrameworkCore;

namespace EventTask.EntityFrameworkCore.Repositories;

public class EventRegistrationRepository : RepositoryBase<EventRegistration, Guid>, IEventRegistrationRepository
{
    private IDbContextProvider<EventTaskDbContext> _dbContextProvider;
    public EventRegistrationRepository(IDbContextProvider<EventTaskDbContext> dbContextProvider) : base(dbContextProvider)
    {
        _dbContextProvider = dbContextProvider;
    }
}
