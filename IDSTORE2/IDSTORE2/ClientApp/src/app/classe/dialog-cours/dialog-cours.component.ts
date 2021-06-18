import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-dialog-cours',
  templateUrl: './dialog-cours.component.html',
  styleUrls: ['./dialog-cours.component.css']
})

interface Classe {
  nom: string;
}


export class DialogCoursComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  

  classes: Classe[] = [
    { nom: 'B1' },
    { nom: 'B2' },
    { nom: 'B3' },
    { nom: 'M1' },
    { nom: 'M2' }
  ];

}
