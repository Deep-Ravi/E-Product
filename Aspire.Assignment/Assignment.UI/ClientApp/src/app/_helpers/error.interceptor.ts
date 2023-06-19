import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';
import { AlertService } from '../_services/alert.service';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private accountService: AccountService,private alertService: AlertService, private router: Router) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            if ([401, 403].includes(err.status) && this.accountService.userValue) { 
                this.alertService.error("You dont have access!!! Please connect to Administrator", { keepAfterRouteChange: true });               
                this.router.navigate(['/home']);         
            }
            debugger;
            const error = err.error?.errors?.join(';') ||err.error.split(":")[1].split("at")[0]||  "Error occured";
            console.error(err);
            return throwError(() => error);
        }))
    }
}