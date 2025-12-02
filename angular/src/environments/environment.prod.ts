import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44339/',
  redirectUri: baseUrl,
  clientId: 'EventTask_App',
  responseType: 'code',
  scope: 'offline_access EventTask',
  requireHttps: true,
};

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'EventTask',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44339',
      rootNamespace: 'EventTask',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
  remoteEnv: {
    url: '/getEnvConfig',
    mergeStrategy: 'deepmerge'
  }
} as Environment;
