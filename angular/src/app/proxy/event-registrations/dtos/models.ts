import type { AuditedEntityDto } from '@abp/ng.core';

export interface EventRegistrationDto extends AuditedEntityDto<string> {
  eventId?: string;
  userId?: string;
  userName?: string;
  registrationDate?: string;
  isCancelled: boolean;
  cancellationDate?: string;
}
