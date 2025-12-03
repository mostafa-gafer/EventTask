using EventTask.EventRegistrations.Dtos;
using EventTask.Events.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EventTask.Events.Interfaces;

public interface IEventAppService :
    ICrudAppService< //Defines CRUD methods
        EventDto, //Used to show events
        Guid, //Primary key of the event event entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateEventDto> //Used to create/update a event
{

}
