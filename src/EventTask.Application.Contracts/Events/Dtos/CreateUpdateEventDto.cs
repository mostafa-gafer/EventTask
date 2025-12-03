using System;
using System.ComponentModel.DataAnnotations;

namespace EventTask.Events.Dtos;

public class CreateUpdateEventDto
{

    [Required]
    [StringLength(EventConsts.MaxNameLength)]
    public string NameEn { get; set; } = string.Empty;

    [Required]
    [StringLength(EventConsts.MaxNameLength)]
    public string NameAr { get; init; } = null!;

    public int? Capacity { get; init; }

    public bool IsOnline { get; init; } = true;

    [Required]
    public DateTime StartDate { get; init; }

    [Required]
    public DateTime EndDate { get; init; }

    [Required]
    public Guid OrganizerId { get; init; }

    [StringLength(EventConsts.MaxLinkLength)]
    public string? Link { get; init; }

    [StringLength(EventConsts.MaxLocationLength)]
    public string? Location { get; init; }

    public bool IsActive { get; init; } = true;
}
