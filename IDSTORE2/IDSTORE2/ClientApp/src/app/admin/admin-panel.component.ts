
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})

export class AdminPanelComponent implements OnInit {

  ngOnInit() {
    this.GetTag();
  }
  public panelOpenState = false;
  public newTagName: string
  public Tags: String[]
  constructor(private router: Router, private http: HttpClient) { }

  public GetTag() {
    this.http.get<String[]>('https://localhost:44373/tag/').subscribe(res => { this.Tags = res; });
  }


  public AddTag(newTagName) {

    if (newTagName == null) return;

    var tt = this.http.get<Boolean>('https://localhost:44373/tag/add/' + newTagName + '/' + "");
    this.GetTag();
  }


  submitted = false;

  onSubmit() { this.submitted = true; }

}
