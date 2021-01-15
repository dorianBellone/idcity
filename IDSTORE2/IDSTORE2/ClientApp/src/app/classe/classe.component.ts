import { Component, OnChanges, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, NavigationStart, Router } from '@angular/router';
import { Fichier } from '../models/fichier';
import { ApiService } from '../services/api.service';
import { MatiereService } from '../services/matiere.service';

@Component({
  selector: 'app-classe',
  templateUrl: './classe.component.html',
  styleUrls: ['./classe.component.css']
})
export class ClasseComponent implements OnInit {
  classe: string;
  classes: string;
  private sub: any;
  data: Fichier[];
  blob: Blob;
  details: Boolean;
  mySubjectVal: string;
  searchText: string;


  constructor(private route: ActivatedRoute, private apiService: ApiService, private router: Router, private matiereService: MatiereService) {
     
    this.route.url.subscribe(url => {
      this.data = [];
      console.log(this.classe);
      this.apiService.getFileByClasse(this.classe)
        .subscribe(
          data => {
            this.data = data;
          });
    });
  }

  ngOnInit() {
    this.details = false;
    if (this.classe == null) {
      this.classe = "";
    }
    this.matiereService.title.subscribe(
      data => {
        this.mySubjectVal = data;
      });
    this.route.paramMap.subscribe(params => {
      this.classe = params.get('classe');
    });
    this.searchText = this.mySubjectVal;
  }

  download(name: string) {
    console.log(name);
    this.apiService.downloadFile(name).subscribe((data) => {

      this.blob = new Blob([data], { type: 'application/pdf' });

      var downloadURL = window.URL.createObjectURL(data);
      var link = document.createElement('a');
      link.href = downloadURL;
      link.download = name;
      link.click();
    });
  }

  displayDetails() {
    this.details = !this.details;
  }
}
