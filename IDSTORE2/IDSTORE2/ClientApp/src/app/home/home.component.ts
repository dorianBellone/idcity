import { Component } from '@angular/core';
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpEvent, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
//import { type } from 'os';
import { ApiService } from '../services/api.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
@Injectable()
export class HomeComponent {
  private baseApiUrl: string;
  private apiDownloadUrl: string;
  private apiUploadUrl: string;
  private apiFileUrl: string;
  private result: string;

  constructor(private httpClient: HttpClient, private apiService: ApiService) {
    this.baseApiUrl = 'https://localhost:44373/api/';
    this.apiDownloadUrl = this.baseApiUrl + 'download';
    this.apiUploadUrl = this.baseApiUrl + 'upload';
    this.apiFileUrl = this.baseApiUrl + 'files';
  }

  public downloadFile(): string  {
    this.apiService.Test().subscribe((data) => this.result = data)
    return this.result;

  }
  public downloadFile2(){
    console.log('OK');
    
  }
  public uploadFile(file: Blob): Observable<HttpEvent<void>> {
    const formData = new FormData();
    formData.append('file', file);

    return this.httpClient.request(new HttpRequest(
      'POST',
      this.apiUploadUrl,
      formData,
      {
        reportProgress: true
      }));
  }

  public getFiles(): Observable<string[]> {
    return this.httpClient.get<string[]>(this.apiFileUrl);
  }

}
