
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Tag } from '../models/tag';

@Component({
  selector: 'admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})

export class AdminPanelComponent implements OnInit {

  ngOnInit() {
    this.GetTag();
    this.tag = new Tag();
  }
  public panelOpenState = false;
  public newTagName: string;
  public Tags: String[];
  public tag: Tag = null;
  public resultAddTag: Boolean;
  constructor(private router: Router, private http: HttpClient) { }

  public GetTag() {
    this.http.get<String[]>('https://localhost:44373/tag/').subscribe(res => { this.Tags = res; });
  }



  // ADD Description
  public AddTag(newTagName : string) {

    if (newTagName == null) return;

    this.tag.name = newTagName;
    
    //this.http.get<Boolean>('https://localhost:44373/tag/add/' + newTagName + '/' + "un").subscribe(res => { this.resultAddTag = res; });

    this.http.post<Boolean>('https://localhost:44373/tag/addtwo/', this.tag).subscribe(res => { this.resultAddTag = res; });
    console.log(this.resultAddTag);

    this.GetTag();




    //this.http.post<any>('https://reqres.in/invalid-url', { title: 'Angular POST Request Example' }).subscribe({
    //  next: data => {
    //    this.postId = data.id;
    //  },

  }
}
