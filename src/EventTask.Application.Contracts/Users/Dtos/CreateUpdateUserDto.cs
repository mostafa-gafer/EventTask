using System;
using System.Collections.Generic;
using System.Text;

namespace EventTask.Users.Dtos;

public class CreateUpdateUserDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}
