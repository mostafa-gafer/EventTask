using EventTask.Events.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTask.Events.Validators;

//public class CreateUpdateEventDtoValidator : AbstractValidator<CreateUpdateEventDto>
//{
//    public CreateUpdateEventDtoValidator()
//    {
//        // ========== Name Validations ==========
//        RuleFor(x => x.NameEn)
//            .NotEmpty().WithMessage("English name is required")
//            .MaximumLength(EventConsts.MaxNameLength)
//            .WithMessage($"English name cannot exceed {EventConsts.MaxNameLength} characters")
//            .MinimumLength(3).WithMessage("English name must be at least 3 characters");

//        RuleFor(x => x.NameAr)
//            .NotEmpty().WithMessage("Arabic name is required")
//            .MaximumLength(EventConsts.MaxNameLength)
//            .WithMessage($"Arabic name cannot exceed {EventConsts.MaxNameLength} characters")
//            .MinimumLength(3).WithMessage("Arabic name must be at least 3 characters")
//            .Matches(@"[\u0600-\u06FF]")
//            .WithMessage("Arabic name must contain Arabic characters");

//        // ========== Date Validations ==========
//        RuleFor(x => x.StartDate)
//            .NotEmpty().WithMessage("Start date is required")
//            .GreaterThanOrEqualTo(DateTime.Now)
//            .WithMessage("Start date must be in the future or today");

//        RuleFor(x => x.EndDate)
//            .NotEmpty().WithMessage("End date is required")
//            .GreaterThan(x => x.StartDate)
//            .WithMessage("End date must be after start date");

//        // Optional: Limit event duration
//        RuleFor(x => x)
//            .Must(x => (x.EndDate - x.StartDate).TotalDays <= 30)
//            .WithMessage("Event duration cannot exceed 30 days")
//            .When(x => x.EndDate > x.StartDate);

//        // ========== Organizer Validation ==========
//        RuleFor(x => x.OrganizerId)
//            .NotEmpty().WithMessage("Organizer is required");

//        // ========== Online Event Validations ==========
//        When(x => x.IsOnline, () =>
//        {
//            RuleFor(x => x.Link)
//                .NotEmpty().WithMessage("Link is required for online events")
//                .MaximumLength(EventConsts.MaxLinkLength)
//                .WithMessage($"Link cannot exceed {EventConsts.MaxLinkLength} characters")
//                .Must(BeAValidUrl).WithMessage("Please provide a valid URL (http or https)");

//            RuleFor(x => x.Capacity)
//                .Null().WithMessage("Capacity should not be set for online events");

//            RuleFor(x => x.Location)
//                .Must(x => string.IsNullOrWhiteSpace(x))
//                .WithMessage("Location should not be set for online events");
//        });

//        // ========== Physical Event Validations ==========
//        When(x => !x.IsOnline, () =>
//        {
//            RuleFor(x => x.Location)
//                .NotEmpty().WithMessage("Location is required for physical events")
//                .MaximumLength(EventConsts.MaxLocationLength)
//                .WithMessage($"Location cannot exceed {EventConsts.MaxLocationLength} characters")
//                .MinimumLength(3).WithMessage("Location must be at least 3 characters");

//            RuleFor(x => x.Capacity)
//                .NotNull().WithMessage("Capacity is required for physical events")
//                .GreaterThan(0).WithMessage("Capacity must be greater than 0")
//                .LessThanOrEqualTo(100000).WithMessage("Capacity cannot exceed 100,000");

//            RuleFor(x => x.Link)
//                .Must(x => string.IsNullOrWhiteSpace(x))
//                .WithMessage("Link should not be set for physical events");
//        });
//    }

//    private bool BeAValidUrl(string? url)
//    {
//        if (string.IsNullOrWhiteSpace(url))
//            return false;

//        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
//               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
//    }
//}