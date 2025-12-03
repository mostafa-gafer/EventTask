using EventTask.Users.Dtos;
using EventTask.Users.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventTask.Users;

public class UserAppService : IUserAppService
{
    private readonly IUserRepository _userRepository;
    public UserAppService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public Task<UserDto> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}
