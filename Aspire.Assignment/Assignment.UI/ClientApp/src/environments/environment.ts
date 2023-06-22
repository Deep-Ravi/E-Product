// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  apiUrl: 'https://localhost:44339',
  tempPassword:'Guest@945',
  defaultRoleId:2,
  defaultOperations:["VIEW"],
  approvalStatus:["NOT SENT","APPROVAL PENDING","APPROVED","REJECTED"],
  proficiency:["BEGINNER","INTERMEDIATE","ADVANCED","EXPERT"],
  roles:["GUEST","ADMIN","DEVELOPER","MANAGER"],
  yearsOfExperience:["0.5-1","1-1.5","1.5-2","2-2.5","2.5-3","3-3.5","3.5-4","4-4.5","4.5-5","5-5.5","5.5-6","6-6.5","6.5-7","7-7.5","7.5-8","8-8.5","8.5-9","9-9.5","9.5-10",">10"]
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
