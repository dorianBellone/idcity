import { Component, OnInit } from '@angular/core';
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpEvent, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiService } from '../services/api.service';
import { File } from '../models/file';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
@Injectable()
export class HomeComponent implements OnInit{
  private baseApiUrl: string;
  private apiDownloadUrl: string;
  private apiUploadUrl: string;
  private apiFileUrl: string;
  private result: File[];



  constructor(private httpClient: HttpClient, private apiService: ApiService) {
    this.baseApiUrl = 'https://localhost:44373/api/';
    this.apiDownloadUrl = this.baseApiUrl + 'download';
    this.apiUploadUrl = this.baseApiUrl + 'upload';
    this.apiFileUrl = this.baseApiUrl + 'files';
  }

  ngOnInit(): void {
    this.apiService.Test().subscribe(
      data =>
        this.apiService.liste = data);
    console.log(this.apiService.liste);
  }




  public downloadFile(): File[]  {
    this.apiService.Test().subscribe((data) => this.result = data)
    return this.result;

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
  public getFiles(): Observable<string[]> {
    return this.httpClient.get<string[]>(this.apiFileUrl);
  }

}
