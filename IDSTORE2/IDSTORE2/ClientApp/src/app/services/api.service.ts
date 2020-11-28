import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { File } from '../models/file';

@Injectable({ providedIn: 'root' })
export class ApiService {

  liste: File[] = [];

  constructor(private http: HttpClient) {
  }

  

  public Test(): Observable<File[]> {
    return this.http.get<File[]>('https://localhost:44373/file/');
  }
}





