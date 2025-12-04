using EventTask.EventRegistrations.Dtos;
using EventTask.EventRegistrations.Interfaces;
using EventTask.Events.Interfaces;
using EventTask.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Users;

namespace EventTask.EventRegistrations;

[Authorize(EventTaskPermissions.Events.Default)]

public class EventRegistrationAppService : ApplicationService, IEventRegistrationAppService
{
    private readonly IEventRepository _eventRepository;
    private readonly IEventRegistrationRepository _registrationRepository;
    private readonly ICurrentUser _currentUser;

    public EventRegistrationAppService(
        IEventRepository eventRepository,
        IEventRegistrationRepository registrationRepository,
        ICurrentUser currentUser)
    {
        _eventRepository = eventRepository;
        _registrationRepository = registrationRepository;
        _currentUser = currentUser;
    }

    [Authorize(Roles = EventTaskConsts.UserRole)]
    public async Task<EventRegistrationDto> RegisterToEventAsync(Guid eventId)
    {
        var userId = _currentUser.Id.Value;

        // Get event with registrations
        var eventEntity = await _eventRepository.GetAsync(
            eventId,
            includeDetails: true
        );

        // Validate event is active
        if (!eventEntity.IsActive)
        {
            throw new BusinessException("EventTask:EventNotActive")
                .WithData("EventId", eventId);
        }

        // Validate event hasn't started
        if (eventEntity.StartDate <= DateTime.UtcNow)
        {
            throw new BusinessException("EventTask:EventAlreadyStarted")
                .WithData("EventId", eventId);
        }

        // Check if user is already registered
        var existingRegistration = await _registrationRepository
            .FindByEventAndUserAsync(eventId, userId);

        if (existingRegistration != null && !existingRegistration.IsCancelled)
        {
            throw new BusinessException("EventTask:AlreadyRegistered")
                .WithData("EventId", eventId);
        }

        // Check capacity for physical events
        if (!eventEntity.IsOnline)
        {
            if (!eventEntity.Capacity.HasValue)
            {
                throw new BusinessException("EventTask:EventCapacityNotSet")
                    .WithData("EventId", eventId);
            }

            var activeRegistrationsCount = await _registrationRepository
                .GetActiveRegistrationsCountAsync(eventId);

            if (activeRegistrationsCount >= eventEntity.Capacity.Value)
            {
                throw new BusinessException("EventTask:EventCapacityReached")
                    .WithData("EventId", eventId)
                    .WithData("Capacity", eventEntity.Capacity.Value);
            }
        }

        var registration = new EventRegistration(
            eventId,
            userId
        );

        await _registrationRepository.InsertAsync(registration);

        return ObjectMapper.Map<EventRegistration, EventRegistrationDto>(registration);

    }

    [Authorize(Roles = EventTaskConsts.UserRole)]
    public async Task CancelRegistrationAsync(Guid eventId)
    {
        var userId = _currentUser.Id.Value;

        var eventEntity = await _eventRepository.GetAsync(eventId);

        // Check if user can cancel (more than 1 hour before start)
        if (!eventEntity.CanCancelRegistration())
        {
            throw new BusinessException("EventTask:CannotCancelRegistration")
                .WithData("EventId", eventId)
                .WithData("StartDate", eventEntity.StartDate)
                .WithData("CancellationDeadline", eventEntity.StartDate.AddHours(-1));
        }

        // Find active registration
        var registration = await _registrationRepository
            .FindByEventAndUserAsync(eventId, userId);

        if (registration == null || registration.IsCancelled)
        {
            throw new BusinessException("EventTask:NotRegisteredToEvent")
                .WithData("EventId", eventId);
        }

        registration.Cancel();
        //await _registrationRepository.UpdateAsync(registration);
    }
}
