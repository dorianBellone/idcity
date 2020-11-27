import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { myObjct } from '../Model/myObjct';

@Injectable({ providedIn: 'root' })
export class ApiService {

 

  constructor(private http: HttpClient) {
  }



  public Test(): Observable<string> {
    return this.http.get('https://localhost:44373/file/', { responseType: 'text' });
  }
}





