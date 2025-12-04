using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace EventTask.Events.Dtos;

public record CreateUpdateEventDto: IValidatableObject
{

    [Required]
    [StringLength(EventConsts.MaxNameLength)]
    public string NameEn { get; init; } = null!;

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

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        // ========== Date Validations ==========

        // Start date must be in the future
        if (StartDate < DateTime.UtcNow)
        {
            yield return new ValidationResult(
                "Start date must be in the future or today.",
                new[] { nameof(StartDate) }
            );
        }

        // End date must be after start date
        if (EndDate <= StartDate)
        {
            yield return new ValidationResult(
                "End date must be after start date.",
                new[] { nameof(EndDate) }
            );
        }

        // Optional: Limit event duration (e.g., max 30 days)
        if ((EndDate - StartDate).TotalDays > 30)
        {
            yield return new ValidationResult(
                "Event duration cannot exceed 30 days.",
                new[] { nameof(StartDate), nameof(EndDate) }
            );
        }

        // ========== Arabic Name Validation ==========

        // Check if Arabic name contains Arabic characters
        if (!string.IsNullOrWhiteSpace(NameAr) && !Regex.IsMatch(NameAr, @"[\u0600-\u06FF]"))
        {
            yield return new ValidationResult(
                "Arabic name must contain Arabic characters.",
                new[] { nameof(NameAr) }
            );
        }

        // Minimum length for Arabic name
        if (!string.IsNullOrWhiteSpace(NameAr) && NameAr.Length < 3)
        {
            yield return new ValidationResult(
                "Arabic name must be at least 3 characters.",
                new[] { nameof(NameAr) }
            );
        }

        // ========== English Name Validation ==========

        // Minimum length for English name
        if (!string.IsNullOrWhiteSpace(NameEn) && NameEn.Length < 3)
        {
            yield return new ValidationResult(
                "English name must be at least 3 characters.",
                new[] { nameof(NameEn) }
            );
        }

        // ========== Online Event Validations ==========

        if (IsOnline)
        {
            // Link is required for online events
            if (string.IsNullOrWhiteSpace(Link))
            {
                yield return new ValidationResult(
                    "Link is required for online events.",
                    new[] { nameof(Link) }
                );
            }
            // Validate URL format
            else if (!IsValidUrl(Link))
            {
                yield return new ValidationResult(
                    "Please provide a valid URL (http or https).",
                    new[] { nameof(Link) }
                );
            }

            // Capacity should not be set for online events
            if (Capacity.HasValue)
            {
                yield return new ValidationResult(
                    "Capacity should not be set for online events.",
                    new[] { nameof(Capacity) }
                );
            }

            // Location should not be set for online events
            if (!string.IsNullOrWhiteSpace(Location))
            {
                yield return new ValidationResult(
                    "Location should not be set for online events.",
                    new[] { nameof(Location) }
                );
            }
        }

        // ========== Physical Event Validations ==========

        if (!IsOnline)
        {
            // Location is required for physical events
            if (string.IsNullOrWhiteSpace(Location))
            {
                yield return new ValidationResult(
                    "Location is required for physical events.",
                    new[] { nameof(Location) }
                );
            }
            // Minimum length for location
            else if (Location.Length < 3)
            {
                yield return new ValidationResult(
                    "Location must be at least 3 characters.",
                    new[] { nameof(Location) }
                );
            }

            // Capacity is required for physical events
            if (!Capacity.HasValue)
            {
                yield return new ValidationResult(
                    "Capacity is required for physical events.",
                    new[] { nameof(Capacity) }
                );
            }
            // Capacity must be positive
            else if (Capacity.Value <= 0)
            {
                yield return new ValidationResult(
                    "Capacity must be greater than 0.",
                    new[] { nameof(Capacity) }
                );
            }
            // Capacity must be reasonable
            else if (Capacity.Value > 100000)
            {
                yield return new ValidationResult(
                    "Capacity cannot exceed 100,000.",
                    new[] { nameof(Capacity) }
                );
            }

            // Link should not be set for physical events
            if (!string.IsNullOrWhiteSpace(Link))
            {
                yield return new ValidationResult(
                    "Link should not be set for physical events.",
                    new[] { nameof(Link) }
                );
            }
        }

        // ========== Organizer Validation ==========

        if (OrganizerId == Guid.Empty)
        {
            yield return new ValidationResult(
                "Organizer is required.",
                new[] { nameof(OrganizerId) }
            );
        }
    }

    private static bool IsValidUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return false;

        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }

}
