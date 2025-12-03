using EventTask.Events.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;

namespace EventTask.Users.Interfaces;

public interface IUserRepository : IRepository<User, Guid>
{
}
