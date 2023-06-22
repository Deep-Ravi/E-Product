import { Component } from '@angular/core';
import { AccountService } from './_services/account.service';
import { User } from './_models/user';
import { AuthService } from './_services/auth.service';
import { environment } from 'src/environments/environment.prod';


@Component({ selector: 'app-root', templateUrl: 'app.component.html' })
export class AppComponent {
    user?: User | null;

    constructor(private accountService: AccountService,private authService:AuthService) {
        this.accountService.user.subscribe(x => this.user = x);
    }
    public isAdmin(): boolean {
        const userRole = this.authService.getUserRole();   
        return userRole === environment.roles[1];
    }
    public isDeveloper(): boolean {
        const userRole = this.authService.getUserRole();   
        return userRole === environment.roles[2];
    }
    public isManager(): boolean {
        const userRole = this.authService.getUserRole();   
        return userRole === environment.roles[3];
    }
    logout() {
        this.accountService.logout();
    }
}