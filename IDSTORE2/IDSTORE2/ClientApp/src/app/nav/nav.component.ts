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
  adminMode: boolean = false;
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

  reflexionB2 = [{ matiere: 'Sciences Appliquées' }, { matiere: 'Algorithmique' }, { matiere: 'Méthodologie' }];
  fonctionnementB2 = [{ matiere: 'Architecture des systèmes d’informations' }, { matiere: 'Virtualisation' }, { matiere: 'Réseaux' }, { matiere: 'Outils Logiciels' }];
  developpementB2 = [{ matiere: 'Langages C' }, { matiere: 'Langages C++' }, { matiere: 'Langages C et C++' }, { matiere: 'Java' }, { matiere: 'C#' }, { matiere: 'PHP' }, { matiere: 'Python' }, { matiere: 'Javascript' }, { matiere: 'Manipulation des données' }];
  environementB2 = [{ matiere: 'Anglais' }, { matiere: 'Economie' }, { matiere: 'Droit' }];
  pratiqueB2 = [{ matiere: 'Projet individuel' }];
  proB2 = [{ matiere: 'Stage' }];

  reflexionB3 = [{ matiere: 'Méthodologie' }];
  fonctionnementB3 = [{ matiere: 'Outils Logiciels' }];
  developpementB3 = [{ matiere: 'Java' }, { matiere: 'C#' }, { matiere: 'PHP' }, { matiere: 'Python' }, { matiere: 'Javascript' }];
  environementB3 = [{ matiere: 'Anglais' }, { matiere: 'Droit' }];
  pratiqueB3 = [{ matiere: 'IDlabs' }, { matiere: 'Présentation du projet Unity' }, { matiere: 'Projet collaboratif' }, { matiere: 'IDcity' }];
  proB3 = [{ matiere: 'Présentation projets en entreprise' }, { matiere: 'Réalisation du projet' }, { matiere: 'Rédaction du mémoire' }, { matiere: 'Soutenance du projet' }];

  reflexionM1 = [{ matiere: 'Les algorithmes pour l’informatique distribuée' }, { matiere: 'La block chain' }, { matiere : 'Méthodologie'}];
  fonctionnementM1 = [{ matiere: 'Outils logiciels' }];
  developpementM1 = [{ matiere: 'Java' }, { matiere: 'C#' }, { matiere: 'PHP' }, { matiere: 'Python' }, { matiere: 'Javascript' }];
  environementM1 = [{ matiere: 'Anglais' }, { matiere: 'Economie' }, { matiere: 'Droit' }, { matiere: 'Culture informatique' }];
  pratiqueM1 = [{ matiere: 'IDlabs' }, { matiere: 'IDcity' }];
  proM1 = [{ matiere: 'Présentation des objectifs du projet individuel en entreprise' }, { matiere: 'Réalisation du projet' }, { matiere: 'Rédaction du mémoire' }, { matiere: 'Soutenance du projet' }];

  reflexionM2 = [{ matiere: 'Méthodologie' }];
  developpementM2 = [{ matiere: 'Langages C' }, { matiere: 'Java' }, { matiere: 'C#' }, { matiere: 'Python' }, { matiere: 'F#' }];
  environementM2 = [{ matiere: 'Anglais' }, { matiere: 'L’aspect financier cadrage d’un projet informatique' }, { matiere: 'Economie' }, { matiere: 'Droit' }];
  pratiqueM2 = [{ matiere: 'IDcity' }];
  proM2 = [{ matiere: 'Présentation des objectifs du projet individuel en entreprise' }, { matiere: 'Réalisation du projet' }, { matiere: 'Rédaction du mémoire' }, { matiere: 'Soutenance du projet' }];




  Matieres2 = [{ matiere: 'Java' }, { matiere: 'CSharp' }, { matiere: 'C' }, { matiere: 'C' }, { matiere: 'C' }, { matiere: 'C' }];

  constructor(private router: Router, private matiereService: MatiereService, private loginService: LoginService, private apiService: ApiService) { }

  ngOnInit() {
    
    this.selectedValue = "Choix de votre classe";
    this.loginService.connectedUser.subscribe(
      data => {
        this.mySubjectVal = data;
        this.mySubjectVal = this.mySubjectVal.substring(0, this.mySubjectVal.indexOf("@"));
        this.mySubjectVal = this.mySubjectVal.replace('.', '\ ');
        console.log(this.mySubjectVal);
        if (this.mySubjectVal === "admin") {
          this.adminMode = true;
          this.loginService.user = this.mySubjectVal;
        }
      }
    );
    this.isLoggedIn$ = this.loginService.isLoggedIn;
    this.adminMode = false;
    this.developpement = false;
    this.reflexion = false;
    this.fonctionnement = false;
    this.environnement = false;
    this.experience = false;
    this.pratique = false;
    this.apiService.getFileByClasse("B1");
   
    
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

