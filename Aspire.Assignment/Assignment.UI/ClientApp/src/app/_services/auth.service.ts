import { Injectable } from '@angular/core';
import jwt_decode from 'jwt-decode';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private accountService: AccountService) { }
  public getUserRole(): string {
    const user = this.accountService.userValue;
    if(user!=null){
      const decodedToken: any = jwt_decode(user!.token!);

      if (decodedToken && decodedToken.role) {
        return decodedToken.role;
      }     
    }   
    return "null";
  }
}