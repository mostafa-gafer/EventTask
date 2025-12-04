using System;
using Volo.Abp.Application.Dtos;

namespace EventTask.Events.Dtos;

public class EventDto : AuditedEntityDto<Guid>
{
    public string? NameEn { get; init; }

    public string? NameAr { get; init; }

    public int? Capacity { get; init; }

    public bool IsOnline { get; init; }

    public DateTime StartDate { get; init; }

    public DateTime EndDate { get; init; }

    public Guid OrganizerId { get; init; }

    public string? Link { get; init; }

    public string? Location { get; init; }

    public bool IsActive { get; init; }
}
