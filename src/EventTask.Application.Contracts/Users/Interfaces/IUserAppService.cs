using EventTask.Events.Dtos;
using EventTask.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EventTask.Users.Interfaces;

public interface IUserAppService
{
    Task<List<UserDto>> GetAllAsync();
}

