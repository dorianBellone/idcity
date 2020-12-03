import { Component, OnInit } from '@angular/core';
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpEvent, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiService } from '../services/api.service';
import { Fichier } from '../models/fichier';
import { BaseDestroyableComponent } from '../common/baseOnInit.component';
import { catchError } from 'rxjs/operators';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
@Injectable()
export class HomeComponent extends BaseDestroyableComponent{
 
  private baseApiUrl: string;
  private apiDownloadUrl: string;
  private apiUploadUrl: string;
  private apiFileUrl: string;
  private data: Fichier[];
  errorMessage: string;


  constructor(private httpClient: HttpClient, private apiService: ApiService) {
    super();
    this.baseApiUrl = 'https://localhost:44373/api/';
    this.apiDownloadUrl = this.baseApiUrl + 'download';
    this.apiUploadUrl = this.baseApiUrl + 'upload';
    this.apiFileUrl = this.baseApiUrl + 'files';
  }

  ngAfterViewInit(): void {}

  ngOnInit(): void {

    this.apiService.Test()
      .subscribe(
      data => {
        this.data = data;
      }
    );
  }



/*
  public downloadFile(): Fichier[]  {
    this.apiService.Test().subscribe((data) => this.data = data)
    return this.data;

  }
  public downloadFile2(){
    console.log('OK');
    
  } /*
  public uploadFile(file: Blob): Observable<HttpEvent<void>> {
    const formData = new FormData();
    const fichier = new File();
    formData.append('file', file);
    
    return this.httpClient.request(new HttpRequest(
      'POST',
      this.apiUploadUrl,
      formData,
      {
        reportProgress: true
      }));
  }
*/

 /* public getFiles(): Observable<string[]> {
    return this.httpClient.get<string[]>(this.apiFileUrl);
  }
  */
}
