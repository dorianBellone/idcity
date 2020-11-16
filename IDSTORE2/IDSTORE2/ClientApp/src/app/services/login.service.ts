import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class LoginService {

  personneConnect: String;
  isLoggedin = false;
  private loggedIn = new BehaviorSubject<boolean>(false); // {1}

  get isLoggedIn() {
    return this.loggedIn.asObservable(); // {2}
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
}
