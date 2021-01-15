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
  selectedValue: string;
  searchText;

  reflexion: boolean = false;
  fonctionnement: boolean = false;
  developpement: boolean = false;
  environnement: boolean = false;
  experience  : boolean = false;
  pratique: boolean = false;
  isLoggedIn$: Observable<boolean>;

  classes: Classe[] = [
    { nom: 'B1' },
    { nom: 'B2' },
    { nom: 'B3' },
    { nom: 'M1' },
    { nom: 'M2' }
  ];

  reflexionB1 = [{ matiere: 'Sciences Appliquées' }, { matiere: 'Algorithmique' }, { matiere: 'Méthodologie' }];
  fonctionnementB1 = [{ matiere: 'Electronique' }, { matiere: 'Architecture des systèmes d’informations' }, { matiere: 'Système d’exploitation' }, { matiere: 'Réseaux' }, { matiere: 'Outils Bureautiques' }];
  developpementB1 = [{ matiere: 'Langages C' }, { matiere: 'PHP' }, { matiere: 'Javascript' }, { matiere: 'Langage pour le web' }, { matiere: 'Manipulation des données' }];
  environementB1 = [{ matiere: 'Anglais' }, { matiere: 'Expresion orale' }, { matiere: 'Expresion écrite' }, { matiere: 'Economie' }, { matiere: 'Droit' }, { matiere: 'Culture informatique' }];
  pratiqueB1 = [{ matiere: 'IDlabs' }, { matiere: 'Projet individuel' }];
  proB1 = [{ matiere: 'Stage' }];


  Matieres2 = [{ matiere: 'Java' }, { matiere: 'CSharp' }, { matiere: 'C' }, { matiere: 'C' }, { matiere: 'C' }, { matiere: 'C' }];

  constructor(private router: Router, private matiereService: MatiereService, private loginService: LoginService, private apiService: ApiService) { }

  ngOnInit() {
    
    this.selectedValue = "Choix de votre classe";
    this.loginService.connectedUser.subscribe(
      data => {
        this.mySubjectVal = data;
        this.mySubjectVal = this.mySubjectVal.substring(0, this.mySubjectVal.indexOf("@"));
        this.mySubjectVal = this.mySubjectVal.replace('.','\ ');
        console.log(data);
      }
    );
    this.isLoggedIn$ = this.loginService.isLoggedIn;
    this.developpement = false;
    this.reflexion = false;
    this.fonctionnement = false;
    this.environnement = false;
    this.experience = false;
    this.pratique = false;
  }

  sendTextValue(matiere) {
    this.matiereService.loadMatiere(matiere);
  }

  sendClasseValue(classe) {
    this.apiService.loadClasse(classe);
    this.router.navigate(['/classe', classe]);
  }

  redirection() {
    this.router.navigate(['/home']);
  }

  displayDeveloppement() {
    this.developpement = !this.developpement;
  }

  displayReflexion() {
    this.reflexion = !this.reflexion;
  }

  displayFonctionnement() {
    this.fonctionnement = !this.fonctionnement;
  }

  displayEnvironnement() {
    this.environnement = !this.environnement;
  }

  displayExperience() {
    this.experience = !this.experience;
  }

  displayPratique() {
    this.pratique = !this.pratique;
  }
}

