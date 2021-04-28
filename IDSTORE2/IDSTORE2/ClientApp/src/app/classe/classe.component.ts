import { Component, OnChanges, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, NavigationStart, Router } from '@angular/router';
import { Fichier } from '../models/fichier';
import { ApiService } from '../services/api.service';
import { LoginService } from '../services/login.service';
import { MatiereService } from '../services/matiere.service';

@Component({
  selector: 'app-classe',
  templateUrl: './classe.component.html',
  styleUrls: ['./classe.component.css']
})
export class ClasseComponent implements OnInit {
  classe: string;
  private sub: any;
  data: Fichier[];
  blob: Blob;
  details: Boolean;
  mySubjectVal: string;
  searchText: string;
  admin: boolean;


  constructor(private route: ActivatedRoute, private apiService: ApiService, private router: Router, private matiereService: MatiereService, private loginService: LoginService) {
     
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
    this.admin = false;
    this.details = false;
    if (this.classe == null) {
      this.classe = "";
    }
    this.matiereService.title.subscribe(
      data => {
        
        this.mySubjectVal = data;
 /*       if (this.mySubjectVal = 'Sciences appliquées') {
          this.mySubjectVal = 'SC';
        }
        if (this.mySubjectVal = 'Algorithmique') {
          this.mySubjectVal = 'ALGO';
        }
        if (data = 'Electronique') {
          data = 'ELEC';
        }
        if (data = 'Méthodologie') {
          data = 'METH';
        }
        if (data = 'Architecture des systèmes d\'informations') {
          data = 'ARCHI';
        }
        if (data = 'Réseaux') {
          data = 'RES';
        }
        if (data = 'Système d\'exploitation') {
          data = 'SYS';
        }
        if (data = 'Outils Bureautiques') {
          data = 'SOFT';
        }
        if (data = 'Langages C') {
          data = 'CCPP';
        }
        if (data = 'Javascript') {
          data = 'JSCRIPT';
        }
        if (data = 'Langage pour le web') {
          data = 'W3C';
        }
        if (data = 'Anglais') {
          data = 'ANG';
        }
        if (data = 'Expresion orale') {
          data = 'ORAL';
        }
        if (data = 'Expresion écrite') {
          data = 'EXPR';
        }
        if (data = 'Economie') {
          data = 'ECO';
        }
        if (data = 'Droit') {
          data = 'DRT';
        }
        if (data = 'Culture informatique') {
          data = 'CULT';
        }*/
      });
    this.route.paramMap.subscribe(params => {
      this.classe = params.get('classe');
    });
    this.searchText = this.mySubjectVal;
    if (this.loginService.user === "admin") {
      this.admin = true;
    }
  }

  download(classe: string, name: string) {
    this.apiService.downloadFile(classe, name).subscribe((data) => {

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
