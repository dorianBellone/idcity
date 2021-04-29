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
        switch (this.mySubjectVal) {
          case 'Sciences Appliquées': {
            this.mySubjectVal = 'SC'
            break;
          }
          case 'Algorithmique': {
            this.mySubjectVal = 'ALGO';
            break;
          }
          case 'Electronique': {
            this.mySubjectVal = 'ELEC';
            break;
          }
          case 'Méthodologie': {
            this.mySubjectVal = 'METH';
            break;
          }
          case 'Architecture des systèmes d\'informations': {
            this.mySubjectVal = 'ARCHI';
            break;
          }
          case 'Réseaux': {
            this.mySubjectVal = 'RES';
            break;
          }
          case 'Système d\'exploitation': {
            this.mySubjectVal = 'SYS';
            break;
          }
          case 'Outils Bureautiques': {
            this.mySubjectVal = 'SOFT';
            break;
          }
          case 'Langages C': {
            this.mySubjectVal = 'CCPP';
            break;
          }
          case 'Javascript': {
            this.mySubjectVal = 'JSCRIPT';
            break;
          }
          case 'Langage pour le web': {
            this.mySubjectVal = 'W3C';
            break;
          }
          case 'Anglais': {
            this.mySubjectVal = 'ANG';
            break;
          }
          case 'Expresion orale': {
            this.mySubjectVal = 'ORAL';
            break;
          }
          case 'Expresion écrite': {
            this.mySubjectVal = 'EXPR';
            break;
          }
          case 'Economie': {
            this.mySubjectVal = 'ECO';
            break;
          }
          case 'Droit': {
            this.mySubjectVal = 'DRT';
            break;
          }
          case 'Culture informatique': {
            this.mySubjectVal = 'CULT';
            break;
          }
    
      





          default: {
            //statements; 
            break;
          }
        } 
       
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
