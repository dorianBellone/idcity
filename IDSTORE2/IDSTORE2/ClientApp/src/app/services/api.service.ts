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
    return this.http.get<Fichier[]>('https://localhost:5001/file/getByClasse/' + this.test);
  }

  public getFile(): Observable<Fichier[]> {
    return this.http.get<Fichier[]>('https://localhost:5001/file/')
  }

  public getFileByClasse(classe : string): Observable<Fichier[]> {
    this.title.subscribe(data => this.test = data);
    classe = this.test;

    return this.http.get<Fichier[]>('https://localhost:5001/file/getByClasse/' + classe);

  }


  public downloadFile(name: string): Observable<Blob>  {
    console.log('https://localhost:5001/file/dl/' + name);
    return this.http.get('https://localhost:5001/file/dl/' + name, { responseType: 'blob' });
  }


}


