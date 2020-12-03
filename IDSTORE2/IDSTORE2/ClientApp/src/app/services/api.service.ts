import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Fichier } from '../models/fichier';

@Injectable({ providedIn: 'root' })
export class ApiService {

  liste: Fichier[] = [];

  constructor(private http: HttpClient) {
  }



  public Test(): Observable<Fichier[]> {
    return this.http.get<Fichier[]>('https://localhost:44373/file/')


  }


}


