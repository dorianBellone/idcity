import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { MatiereService } from '../services/matiere.service';
import { LoginService } from '../services/login.service';
import { ApiService } from '../services/api.service';

interface Classe {
  nom: string;
}

interface Matiere {
  nom: string
}

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  mySubjectVal: string;
  developpement: boolean = false;
  selectedValue: string;
  searchText;

  isLoggedIn$: Observable<boolean>;

  classes: Classe[] = [
    { nom: 'B1' },
    { nom: 'B2' },
    { nom: 'B3' },
    { nom: 'M1' },
    { nom: 'M2' }
  ];

  Matieres = [{ matiere: 'Java' }, { matiere: 'CSharp' }, { matiere: 'C' }];

  constructor(private router: Router, private matiereService: MatiereService, private loginService: LoginService, private apiService: ApiService) { }

  ngOnInit() {
    this.selectedValue = "Choix de votre classe";
    this.loginService.connectedUser.subscribe(
      data => {
        this.mySubjectVal = data;
        console.log(data);
      }
    );
    this.isLoggedIn$ = this.loginService.isLoggedIn;
    this.developpement = false;
  }

  sendTextValue(matiere) {
    this.matiereService.loadMatiere(matiere);
    this.router.navigate(['/liste']);
  }

  sendClasseValue(classe) {
    this.apiService.loadClasse(classe);
   // this.router.navigate(['/liste/']);
    this.router.navigate(['/classe', classe]);
  }

  redirection() {
    this.router.navigate(['/home']);
  }

  displayDeveloppement() {
    this.developpement = !this.developpement;
  }


}

