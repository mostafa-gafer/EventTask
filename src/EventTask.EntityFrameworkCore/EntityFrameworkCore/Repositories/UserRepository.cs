using EventTask.Users;
using EventTask.Users.Interfaces;
using System;
using Volo.Abp.EntityFrameworkCore;

namespace EventTask.EntityFrameworkCore.Repositories;

public class UserRepository : RepositoryBase<User, Guid>, IUserRepository
{
    private IDbContextProvider<EventTaskDbContext> _dbContextProvider;
    public UserRepository(IDbContextProvider<EventTaskDbContext> dbContextProvider) : base(dbContextProvider)
    {
        _dbContextProvider = dbContextProvider;
    }
}
