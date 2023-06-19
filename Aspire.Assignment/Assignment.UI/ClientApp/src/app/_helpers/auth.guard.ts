import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, CanLoad } from '@angular/router';
import { AccountService } from '../_services/account.service';


@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanLoad,CanActivate {
    constructor(
        private router: Router,
        private accountService: AccountService
    ) {}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const user = this.accountService.userValue;
        if (user) {
            // authorised so return true
            return true;
        }

        // not logged in so redirect to login page with the return url
        this.router.navigate(['/account/login'], { queryParams: { returnUrl: state.url }});
        return false;
    }
    canLoad() {
        const user = this.accountService.userValue;
        if (user) {
            // authorised so return true
            return true;
        }
        window.alert("You dont have access!!! Please connect to Administrator");
        this.router.navigate(['/account/login']);
        return false;
      }
}