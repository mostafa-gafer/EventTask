import type { AuditedEntityDto } from '@abp/ng.core';

export interface UserDto extends AuditedEntityDto<string> {
  name?: string;
}
