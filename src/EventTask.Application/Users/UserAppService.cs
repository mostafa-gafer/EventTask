using EventTask.Events.Dtos;
using EventTask.Events.Entities;
using EventTask.Permissions;
using EventTask.Users.Dtos;
using EventTask.Users.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;

namespace EventTask.Users;

[Authorize]
public class UserAppService : ApplicationService, IUserAppService
{
    private readonly IIdentityUserRepository _userRepository;
    public UserAppService(IIdentityUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<List<UserDto>> GetAllAsync()
    {
        var result = await _userRepository.GetListByNormalizedRoleNameAsync(EventTaskConsts.UserNormalizedRole);
        return result.Select(x => new UserDto
        {
            Id = x.Id,
            Name = x.Name,
        }).ToList();
    }
}
