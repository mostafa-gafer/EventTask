import type { AuditedEntityDto } from '@abp/ng.core';

export interface CreateUpdateEventDto {
  nameEn: string;
  nameAr: string;
  capacity?: number;
  isOnline: boolean;
  startDate: string;
  endDate: string;
  organizerId: string;
  link?: string;
  location?: string;
  isActive: boolean;
}

export interface EventDto extends AuditedEntityDto<string> {
  nameEn?: string;
  nameAr?: string;
  capacity?: number;
  isOnline: boolean;
  startDate?: string;
  endDate?: string;
  organizerId?: string;
  organizerName?: string;
  link?: string;
  location?: string;
  isActive: boolean;
}
