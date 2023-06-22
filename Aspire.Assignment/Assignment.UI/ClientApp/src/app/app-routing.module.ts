import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_helpers/auth.guard';
import { AlertComponent } from './_components/alert.component';


const accountModule = () => import('./account/account.module').then(x => x.AccountModule);
const appsModule = () => import('./apps/apps.module').then(x => x.AppsModule);
const productsModule = () => import('./products/products.module').then(x => x.ProductsModule);
const skillsetModule = () => import('./skillset/skillset.module').then(x => x.SkillSetModule);
const skillapprovalModule = () => import('./approval/skillapproval.module').then(x => x.SkillApprovalModule);
const usersModule = () => import('./users/users.module').then(x => x.UsersModule);

const routes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'apps', loadChildren: appsModule, canLoad:[AuthGuard]},
    { path: 'account', loadChildren: accountModule },
    { path: 'products', loadChildren: productsModule, canLoad:[AuthGuard] },
    { path: 'skillset', loadChildren: skillsetModule, canLoad:[AuthGuard] },
    { path: 'approval', loadChildren: skillapprovalModule, canLoad:[AuthGuard] },
    { path: 'users', loadChildren: usersModule, canLoad:[AuthGuard]},
    // otherwise redirect to home
    { path: '**', redirectTo: '' },
    { path: 'alert', component: AlertComponent},
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }