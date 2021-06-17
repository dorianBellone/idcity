import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService } from '../services/login.service';
 // ADD BR Test upload 
import { Output, EventEmitter } from '@angular/core';
import { HttpEventType, HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private loginservice: LoginService, private router: Router,  /*ADD BR Test upload*/ private http: HttpClient) { }

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

   // ADD BR Test upload
  public progress: number;
  public message: string;
  @Output() public onUploadFinished = new EventEmitter();
  public uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.http.post('https://localhost:44373/file/upload', formData, { reportProgress: true, observe: 'events' })
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.message = 'Upload success.';
          this.onUploadFinished.emit(event.body);
        }
      });
  }
}
