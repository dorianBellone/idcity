import {
  CanActivate, ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router
} from '@angular/router';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { map, take } from 'rxjs/operators';
import { LoginService } from './login.service';
@Injectable()

export class AuthGuard implements CanActivate {

  constructor(private loginservice: LoginService, private router: Router) { }

   canActivate(route: ActivatedRouteSnapshot,state: RouterStateSnapshot):
    boolean
    {
       if (this.loginservice.isLoggedin) {
           // if we return true user is allowed to access that route
           return true;
       } else {
           // if we return false user is not allowed to access
           return false;
       }
   }
 
}
