// import { Injector, ElementRef } from "@angular/core";
// import { AppConsts } from "@shared/AppConsts";
// import {
//   LocalizationService,
//   PermissionCheckerService,
//   FeatureCheckerService,
//   NotifyService,
//   SettingService,
//   MessageService,
//   AbpMultiTenancyService,
// } from "abp-ng2-module";

// import { AppSessionService } from "@shared/session/app-session.service";
// import { API_BASE_URL } from "./service-proxies/service-proxies";

// export abstract class AppComponentBase {
//   localizationSourceName = AppConsts.localization.defaultLocalizationSourceName;

//   localization: LocalizationService;
//   permission: PermissionCheckerService;
//   feature: FeatureCheckerService;
//   notify: NotifyService;
//   setting: SettingService;
//   message: MessageService;
//   multiTenancy: AbpMultiTenancyService;
//   appSession: AppSessionService;
//   elementRef: ElementRef;
//   baseUrl: string;

//   constructor(injector: Injector) {
//     this.localization = injector.get(LocalizationService);
//     this.permission = injector.get(PermissionCheckerService);
//     this.feature = injector.get(FeatureCheckerService);
//     this.notify = injector.get(NotifyService);
//     this.setting = injector.get(SettingService);
//     this.message = injector.get(MessageService);
//     this.multiTenancy = injector.get(AbpMultiTenancyService);
//     this.appSession = injector.get(AppSessionService);
//     this.elementRef = injector.get(ElementRef);
//     this.baseUrl = injector.get(API_BASE_URL);
//   }

//   l(key: string, ...args: any[]): string {
//     let localizedText = this.localization.localize(
//       key,
//       this.localizationSourceName
//     );

//     if (!localizedText) {
//       localizedText = key;
//     }

//     if (!args || !args.length) {
//       return localizedText;
//     }

//     args.unshift(localizedText);
//     return abp.utils.formatString.apply(this, args);
//   }

//   isGranted(permissionName: string): boolean {
//     return this.permission.isGranted(permissionName);
//   }

//   public getLocaliztionByName(columnName: string) {
//     const langName = this.toPascalCase(this.localization.currentLanguage.name);
//     return `${columnName}${langName}`;
//   }

//   public toPascalCase(str: string): string {
//     return str
//       .replace(/(\w)(\w*)/g, function (g0, g1, g2) {
//         return g1.toUpperCase() + g2.toLowerCase();
//       })
//       .replace(/\s/g, "")
//       .replace(/^(.)/, function (g) {
//         return g.toUpperCase();
//       });
//   }

//   getImageUrl(imageName: string, isThumb: boolean = false) {
//     if (isThumb) return `${this.baseUrl}/images/public/thumb_${imageName}`;
//     else return `${this.baseUrl}/images/${imageName}`;
//   }
// }
