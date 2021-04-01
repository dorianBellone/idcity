import { HttpClient, HttpEvent, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Fichier } from '../models/fichier';
import { NgxFileSaverService } from '@clemox/ngx-file-saver';

@Injectable({ providedIn: 'root' })
export class ApiService {
  private apiDownloadUrl: string;
  liste: Fichier[] = [];
  result: BlobPart[] = [];
  public title = new Subject<string>();
  test: string = "";

  constructor(private http: HttpClient) {
  }

  loadClasse(data): Observable<Fichier[]> {
    this.title.next(data);
    this.title.subscribe(data => this.test = data);
    console.log(this.test);
    //return this.http.get<Fichier[]>('https://localhost:3031/file/getByClasse/' + this.test);

    return this.http.get<Fichier[]>('http://localhost:3030/file/getByClasse/' + this.test);
  }

  public getFile(): Observable<Fichier[]> {
    return this.http.get<Fichier[]>('http://localhost:3030/file/')
  }

  public getFileByClasse(classe : string): Observable<Fichier[]> {
    this.title.subscribe(data => this.test = data);
    classe = this.test;

    //return this.http.get<Fichier[]>('https://localhost:3030/file/getByClasse/' + classe);
    return this.http.get<Fichier[]>('http://idboard.net:45005/file/getByClasse/' + classe);

  }


  public downloadFile(name: string): Observable<Blob>  {
    console.log('http://localhost:3030/file/dl/' + name);
    return this.http.get('http://localhost:3030/file/dl/' + name, { responseType: 'blob' });
  }

  //public TEST_get(): String {
  //  return this.http.get<String>('http://localhost:3030/test/').subscribe;
  //}

}


