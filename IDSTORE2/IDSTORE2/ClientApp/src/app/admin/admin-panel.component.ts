
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
  public desc : string;

  public Tags: Tag[];
  public tag: Tag = null;
  public resultAddTag: Boolean;
  constructor(private router: Router, private http: HttpClient) { }

  public GetTag() {
    this.http.get<Tag[]>('https://localhost:44373/tag/').subscribe(res => { this.Tags = res; });
  }

  public AddTag(newTagName: string, _description: string) {

    if (newTagName == null) return;
    this.tag.name = newTagName;
    this.tag.description = _description;

    this.http.post<Boolean>('https://localhost:44373/tag/add/', this.tag).subscribe(res => { this.resultAddTag = res; });
    this.GetTag();
  }
}
