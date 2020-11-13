import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService } from '../service/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private loginservice : LoginService,private router: Router) {}
    
  erreur = true;
  password = "";
  login = "";
  loginForm: FormGroup;

  ngOnInit(): void {
  }

  handleLogin() {
    this.loginservice.personneConnect = this.login;
    /*if (this.password === "admin") {
      this.loginservice.login();
    }
    else {
      this.erreur = false
    }
  }*/
  this.loginservice.login();
  }
}