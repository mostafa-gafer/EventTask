using EventTask.EventRegistrations;
using EventTask.EventRegistrations.Interfaces;
using EventTask.Events.Entities;
using EventTask.Events.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace EventTask.EntityFrameworkCore.Repositories;

public class EventRegistrationRepository : RepositoryBase<EventRegistration, Guid>, IEventRegistrationRepository
{
    private IDbContextProvider<EventTaskDbContext> _dbContextProvider;
    public EventRegistrationRepository(IDbContextProvider<EventTaskDbContext> dbContextProvider) : base(dbContextProvider)
    {
        _dbContextProvider = dbContextProvider;
    }

    public async Task<EventRegistration?> FindByEventAndUserAsync(Guid eventId, Guid userId)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .Where(r => r.EventId == eventId && r.UserId == userId)
            .OrderByDescending(r => r.RegisteredAt)
            .FirstOrDefaultAsync();
    }

    public async Task<int> GetActiveRegistrationsCountAsync(Guid eventId)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .Where(r => r.EventId == eventId && !r.IsCancelled)
            .CountAsync();
    }

    public async Task<bool> IsUserRegisteredAsync(Guid eventId, Guid userId)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .AnyAsync(r => r.EventId == eventId && r.UserId == userId && !r.IsCancelled);
    }
}
