import { Component } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { TokenResponse, User } from '../_models/user';
import { AuthService } from '../_services/auth.service';
import { environment } from 'src/environments/environment';
import { Subscription } from 'rxjs';
import { AlertService } from '../_services/alert.service';
import { Router } from '@angular/router';
import { SkillSetService } from '../_services/skillset.service';


@Component({ templateUrl: 'home.component.html' })
export class HomeComponent {
    user: TokenResponse | null;
    subscription!:Subscription;
    constructor(private accountService: AccountService,private authService:AuthService,private alertService: AlertService,private router: Router,private skillSetService:SkillSetService) {
        this.user = this.accountService.userValue;
    }
    ngOnInit() {
        if(this.isManager())
        {
            this.subscription=this.skillSetService.getNotifyNewSkillSet().subscribe({
                next: (data) => {
                  this.alertService.success(data+" New Skills Has Been Added", { keepAfterRouteChange: true });
                  this.router.navigateByUrl('/home');
                },
                error: (error: any) => {
                  this.alertService.error(error);
                }
              });
        }
      }
    public isManager(): boolean {
        const userRole = this.authService.getUserRole();   
        return userRole === environment.roles[3];
    }
    ngOnDestroy(): void {
        if(this.subscription){
          this.subscription.unsubscribe();
        }
      }
}