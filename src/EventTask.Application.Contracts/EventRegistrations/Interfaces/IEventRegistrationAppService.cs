using EventTask.EventRegistrations.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EventTask.EventRegistrations.Interfaces;

public interface IEventRegistrationAppService : IApplicationService
{
    Task<List<EventRegistrationDto>> GetEventRegistrationsAsync(Guid eventId);
    Task<EventRegistrationDto> RegisterToEventAsync(Guid eventId);
    Task CancelRegistrationAsync(Guid eventId);
}
