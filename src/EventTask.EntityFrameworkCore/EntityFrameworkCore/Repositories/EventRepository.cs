using EventTask.Events.Entities;
using EventTask.Events.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Specifications;

namespace EventTask.EntityFrameworkCore.Repositories;

public class EventRepository : RepositoryBase<Event, Guid>, IEventRepository
{
    private IDbContextProvider<EventTaskDbContext> _dbContextProvider;
    public EventRepository(IDbContextProvider<EventTaskDbContext> dbContextProvider) : base(dbContextProvider)
    {
        _dbContextProvider = dbContextProvider;
    }
}
