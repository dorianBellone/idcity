import {
  CanActivate, ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router
} from '@angular/router';
import { Injectable } from '@angular/core';
import { LoginService } from './service/login.service';
import { Observable } from 'rxjs';

import { map, take } from 'rxjs/operators';
@Injectable()

export class AuthGuard implements CanActivate {

  constructor(private loginservice: LoginService, private router: Router) { }

  /* canActivate(route: ActivatedRouteSnapshot,state: RouterStateSnapshot):
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
 */
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
    return this.loginservice.isLoggedIn         // {1}
      .pipe(
        take(1),                              // {2} 
        map((isLoggedIn: boolean) => {         // {3}
          if (!isLoggedIn) {
            this.router.navigate(['/login']);  // {4}
            return false;
          }
          this.router.navigate(['/home']);
          return true;
        }));
  }
}
