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



  public Test(): Observable<Fichier[]> {
    return this.http.get<Fichier[]>('https://localhost:44373/file/')
  }

  public getFile(): Observable<Blob> {
    let path = 'https://localhost:44373/file/dl';
    return this.http.get(path, { responseType: 'blob' })
  }

  public get() {
    this.fileSaver.saveUrl('https://localhost:44373/file/getget', 'mon_fichier.pdf');
  }

  public getgetget() {
   
    this.http.get<BlobPart[]>('https://localhost:44373/file/getgetget').subscribe(tt => this.result = tt);
    const file = new Blob(this.result, { type: 'application/pdf' });
    this.fileSaver.saveBlob(file, "test.pdf");
  }

  public downloadFile(name: string): Observable<Blob>  {
    console.log('https://localhost:44373/file/dl/' + name);
    return this.http.get('https://localhost:44373/file/dl/' + name, { responseType: 'blob' });
  }
}


