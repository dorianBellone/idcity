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
  styleUrls: ['./home.component.css'],
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

  ngOnInit(): void {}
}


