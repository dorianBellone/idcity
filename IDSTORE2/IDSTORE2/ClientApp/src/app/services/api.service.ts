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
  constructor(private http: HttpClient, private fileSaver: NgxFileSaverService) {
  }

  public getFile(): Observable<Fichier[]> {
    return this.http.get<Fichier[]>('https://localhost:44373/file/')
  }

  public getFileByClasse(classe: String): Observable<Fichier[]> {
    return this.http.get<Fichier[]>('https://localhost:44373/file/getByClasse/' + classe);
  }


  public downloadFile(name: string): Observable<Blob>  {
    console.log('https://localhost:44373/file/dl/' + name);
    return this.http.get('https://localhost:44373/file/dl/' + name, { responseType: 'blob' });
  }


}


