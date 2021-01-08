import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ApiService } from './services/api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';



  constructor(private httpClient: HttpClient, private apiService: ApiService) { }

  ngOnInit(): void {
    this.apiService.getFile().subscribe(
      data => this.apiService.liste = data);
    console.log("app component");
    console.log(this.apiService.liste);
  }

}
