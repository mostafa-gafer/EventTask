using EventTask.EventRegistrations;
using EventTask.Users;
using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace EventTask.Events.Entities;

public class Event : AuditedAggregateRoot<Guid>
{
    public string NameEn { get; private set; }

    public string NameAr { get; private set; }

    public int? Capacity { get; private set; }

    public bool IsOnline { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }

    public Guid OrganizerId { get; private set; }

    public User Organizer { get; private set; }

    public string Link { get; private set; }

    public string Location { get; private set; }

    public bool IsActive { get; private set; }


    private readonly List<EventRegistration> _registrations = new();

    public IReadOnlyCollection<EventRegistration> Registrations => _registrations.AsReadOnly();

    protected Event() { }

    public Event(
        Guid id,
        string nameEn,
        string nameAr,
        bool isOnline,
        DateTime startDate,
        DateTime endDate,
        Guid organizerId,
        int? capacity = null,
        string link = null,
        string location = null,
        bool isActive = true) : base(id)
    {
        SetName(nameEn, nameAr);
        SetDates(startDate, endDate);
        SetEventType(isOnline, capacity, link, location);
        OrganizerId = organizerId;
        IsActive = isActive;
    }

    public void Update(
        string nameEn,
        string nameAr,
        bool isOnline,
        DateTime startDate,
        DateTime endDate,
        int? capacity = null,
        string link = null,
        string location = null)
    {
        SetName(nameEn, nameAr);
        SetDates(startDate, endDate);
        SetEventType(isOnline, capacity, link, location);
    }

    public void SetActive(bool isActive)
    {
        IsActive = isActive;
    }

    public bool CanRegister()
    {
        if (!IsActive) return false;
        if (DateTime.Now >= StartDate) return false;
        if (!IsOnline && Capacity.HasValue)
        {
            return _registrations.Count < Capacity.Value;
        }
        return true;
    }

    public bool CanCancelRegistration()
    {
        return DateTime.Now < StartDate.AddHours(-1);
    }

    internal void AddRegistration(EventRegistration registration)
    {
        _registrations.Add(registration);
    }

    internal void RemoveRegistration(EventRegistration registration)
    {
        _registrations.Remove(registration);
    }

    private void SetName(string nameEn, string nameAr)
    {
        NameEn = Check.NotNullOrWhiteSpace(nameEn, nameof(nameEn), EventConsts.MaxNameLength);
        NameAr = Check.NotNullOrWhiteSpace(nameAr, nameof(nameAr), EventConsts.MaxNameLength);
    }

    private void SetDates(DateTime startDate, DateTime endDate)
    {
        if (endDate <= startDate)
            throw new BusinessException(EventDomainErrorCodes.InvalidDateRange);

        StartDate = startDate;
        EndDate = endDate;
    }

    private void SetEventType(bool isOnline, int? capacity, string link, string location)
    {
        IsOnline = isOnline;

        if (isOnline)
        {
            Link = Check.NotNullOrWhiteSpace(link, nameof(link), EventConsts.MaxLinkLength);
            Location = null;
            Capacity = null;
        }
        else
        {
            Location = Check.NotNullOrWhiteSpace(location, nameof(location), EventConsts.MaxLocationLength);
            if (capacity.HasValue && capacity.Value <= 0)
                throw new BusinessException(EventDomainErrorCodes.InvalidCapacity);
            Capacity = capacity;
            Link = null;
        }
    }
}