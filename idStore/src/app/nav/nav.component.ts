
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatiereService } from '../service/matiere.service';

interface Classe {
  nom: string;
  valeur: string;
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

  
  developpement: boolean = false;
  selectedValue: string;
  searchText;

  classes: Classe[] = [
    { nom: 'B1', valeur: '1'},
    { nom: 'B2', valeur: '2'},
    { nom: 'B3', valeur: '3'},
    { nom: 'M1', valeur: '4'},
    { nom: 'M2', valeur: '5'}
  ];

  Matieres= [{matiere: 'Java'},{matiere: 'CSharp'},{matiere: 'C'}];    

  constructor( private router: Router,private matiereService: MatiereService) {}

  ngOnInit() {
   this.developpement = false;

  
  }

  sendTextValue(matiere){
    this.matiereService.loadMatiere(matiere);
    this.router.navigate(['/liste']);
  }

  redirection(){
    this.router.navigate(['/home']);
  }

  displayDeveloppement() {
    this.developpement = !this.developpement;
  }


}