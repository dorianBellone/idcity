import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class LoginService {

  public connectedUser = new Subject<string>();;
  isLoggedin = false;
  private loggedIn = new BehaviorSubject<boolean>(false);
  get isLoggedIn() {
    return this.loggedIn.asObservable();
  }

  constructor(
    private router: Router
  ) { }

  login() {
    this.isLoggedin = true;
    this.loggedIn.next(true);
    this.router.navigate(['/home']);
  }

  logout() {
    this.isLoggedin = false;
  }

  loadUser(data) {
    this.connectedUser.next(data);
  }
}





