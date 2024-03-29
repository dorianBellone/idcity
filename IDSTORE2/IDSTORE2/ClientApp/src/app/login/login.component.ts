import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService } from '../services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private loginservice: LoginService, private router: Router) { }

  erreur = true;
  password = "";
  login = "";
  loginForm: FormGroup;

  ngOnInit(): void {
  }

  handleLogin(data) {
    if (this.password === "admin") {
      this.sendTextValue(data);
      this.loginservice.login();
    }
    else {
      this.erreur = false
    }
  
    //this.loginservice.login();
  }

  sendTextValue(data) {
    this.loginservice.loadUser(data);
  }

}
