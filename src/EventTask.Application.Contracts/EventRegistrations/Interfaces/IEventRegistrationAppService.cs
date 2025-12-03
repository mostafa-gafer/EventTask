using EventTask.Books;
using EventTask.EventRegistrations.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EventTask.EventRegistrations.Interfaces;

public interface IEventRegistrationAppService :
    ICrudAppService< //Defines CRUD methods
        EventRegistrationDto, //Used to show registrations
        Guid, //Primary key of the event register entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateEventRegistrationDto> //Used to create/update a registration
{

}
