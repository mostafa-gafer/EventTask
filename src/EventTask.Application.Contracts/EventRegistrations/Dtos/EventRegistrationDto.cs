using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EventTask.EventRegistrations.Dtos;

public class EventRegistrationDto : AuditedEntityDto<Guid>
{
    public Guid EventId { get; set; }
    public Guid UserId { get; set; }
    public Guid UserName { get; set; }
    public DateTime RegistrationDate { get; set; }
    public bool IsCancelled { get; set; }
    public DateTime? CancellationDate { get; set; }

}
