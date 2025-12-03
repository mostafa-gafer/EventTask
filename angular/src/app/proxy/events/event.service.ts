import type { CreateUpdateEventDto, EventDto } from './dtos/models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable, inject } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class EventService {
  private restService = inject(RestService);
  apiName = 'Default';
  

  create = (input: CreateUpdateEventDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EventDto>({
      method: 'POST',
      url: '/api/app/event',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/event/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EventDto>({
      method: 'GET',
      url: `/api/app/event/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<EventDto>>({
      method: 'GET',
      url: '/api/app/event',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateEventDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EventDto>({
      method: 'PUT',
      url: `/api/app/event/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });
}