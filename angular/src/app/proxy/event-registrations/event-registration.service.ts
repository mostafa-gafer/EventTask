import type { EventRegistrationDto } from './dtos/models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable, inject } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class EventRegistrationService {
  private restService = inject(RestService);
  apiName = 'Default';
  

  cancelRegistration = (eventId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/event-registration/cancel-registration/${eventId}`,
    },
    { apiName: this.apiName,...config });
  

  getEventRegistrations = (eventId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EventRegistrationDto[]>({
      method: 'GET',
      url: `/api/app/event-registration/event-registrations/${eventId}`,
    },
    { apiName: this.apiName,...config });
  

  registerToEvent = (eventId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EventRegistrationDto>({
      method: 'POST',
      url: `/api/app/event-registration/register-to-event/${eventId}`,
    },
    { apiName: this.apiName,...config });
}