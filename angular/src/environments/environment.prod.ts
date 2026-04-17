import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'JobInsight',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44312/',
    redirectUri: baseUrl,
    clientId: 'JobInsight_App',
    responseType: 'code',
    scope: 'offline_access JobInsight',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44312',
      rootNamespace: 'JobInsight',
    },
  },
} as Environment;
