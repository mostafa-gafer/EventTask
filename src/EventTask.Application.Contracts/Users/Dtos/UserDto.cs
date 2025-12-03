using System;
using Volo.Abp.Application.Dtos;

namespace EventTask.Users.Dtos;

public class UserDto : AuditedEntityDto<Guid>
{
    public string? Name { get; set; }
}
