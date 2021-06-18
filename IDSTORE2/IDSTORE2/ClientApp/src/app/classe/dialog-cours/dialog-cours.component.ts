import { Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { HttpEventType, HttpClient } from '@angular/common/http';
import { MAT_DIALOG_DATA } from '@angular/material';
interface Classe {
  nom: string;
}
interface Matiere {
  matiere: string;
}

@Component({
  selector: 'app-dialog-cours',
  templateUrl: './dialog-cours.component.html',
  styleUrls: ['./dialog-cours.component.css']
})


export class DialogCoursComponent implements OnInit {
  @Output() public onUploadFinished = new EventEmitter();

  constructor( private http: HttpClient) { }
  selectedValue: string;
  selectedValueM: string;
  public progress: number;
  public message: string;
  public ok: string;

  ngOnInit() {
  }

  matieresAll: Matiere[] = [{ matiere: 'Sciences Appliqu�es' }, { matiere: 'Algorithmique' }, { matiere: 'M�thodologie' }, { matiere: 'Electronique' }, { matiere: 'Architecture des syst�mes d�informations' }, { matiere: 'Syst�me d�exploitation' }, { matiere: 'R�seaux' }, { matiere: 'Outils Bureautiques' },
  { matiere: 'Langages C' }, { matiere: 'PHP' }, { matiere: 'Javascript' }, { matiere: 'Langage pour le web' }, { matiere: 'Manipulation des donn�es' }, { matiere: 'Anglais' }, { matiere: 'Expresion orale' }, { matiere: 'Expresion �crite' }, { matiere: 'Economie' }, { matiere: 'Droit' }, { matiere: 'Culture informatique' },
  { matiere: 'IDlabs' }, { matiere: 'Projet individuel' }, { matiere: 'Stage' }];


  classes: Classe[] = [
    { nom: 'B1' },
    { nom: 'B2' },
    { nom: 'B3' },
    { nom: 'M1' },
    { nom: 'M2' }
  ];

  

  public AddCours = (name,classe, files) => {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.http.post('https://localhost:44373/file/upload/' + classe +"/"+ name, formData, { reportProgress: true, observe: 'events' })
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.message = 'Upload success.';
          this.onUploadFinished.emit(event.body);
        }
      });
  }

  public check(value) :String {
    switch (this.selectedValueM) {
      case 'Sciences Appliqu�es': {
        value = 'SC'
        break;
      }
      case 'Algorithmique': {
        value = 'ALGO';
        break;
      }
      case 'Electronique': {
        value = 'ELEC';
        break;
      }
      case 'M�thodologie': {
        value = 'METH';
        break;
      }
      case 'Architecture des syst�mes d\'informations': {
        value = 'ARCHI';
        break;
      }
      case 'R�seaux': {
        value = 'RES';
        break;
      }
      case 'Syst�me d\'exploitation': {
        value = 'SYS';
        break;
      }
      case 'Outils Bureautiques': {
        value = 'SOFT';
        break;
      }
      case 'Langages C': {
        value = 'CCPP';
        break;
      }
      case 'Javascript': {
        value = 'JSCRIPT';
        break;
      }
      case 'Langage pour le web': {
        value = 'W3C';
        break;
      }
      case 'Anglais': {
        value = 'ANG';
        break;
      }
      case 'Expresion orale': {
        value = 'ORAL';
        break;
      }
      case 'Expresion �crite': {
        value = 'EXPR';
        break;
      }
      case 'Economie': {
        value = 'ECO';
        break;
      }
      case 'Droit': {
        value = 'DRT';
        break;
      }
      case 'Culture informatique': {
        value = 'CULT';
        break;
      }
      default: {
        //statements; 
        break;
      }
    }
    this.ok = value + '_';
    console.log(this.ok);
    return value;
  }
  


  test(name,classe) {
    console.log(name);
    console.log(classe);
  }

}
