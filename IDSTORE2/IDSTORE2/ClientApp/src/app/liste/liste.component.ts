import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgxFileSaverService } from '@clemox/ngx-file-saver';
import { BaseDestroyableComponent } from '../common/baseOnInit.component';
import { Fichier } from '../models/fichier';
import { ApiService } from '../services/api.service';
import { MatiereService } from '../services/matiere.service';
import { saveAs } from 'file-saver';
import { Router } from '@angular/router';
import { ClasseService } from '../services/classe.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-liste',
  templateUrl: './liste.component.html',
  styleUrls: ['./liste.component.css']
})
export class ListeComponent extends BaseDestroyableComponent {
  mySubjectVal: string;
 // @Input('myInputVal') myInputVal: string;
  //@Output('myOutputVal') myOutputVal = new EventEmitter();
  matiere: String;
  data: Fichier[];
  da: any;
  blob: Blob;
  classe: string;



  constructor(private matiereService: MatiereService, private apiService: ApiService, private classeService : ClasseService) {
    super();
  }

  ngAfterViewInit(): void {
    this.matiereService.title.subscribe(
      data => {
        this.mySubjectVal = data;
      });
  }

  ngOnInit(): void {
    this.apiService.getFile()
      .subscribe(
        data => {
          this.data = data;
        }
    );
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

  ngOnDestroy() {
    console.log("inside child component ngOnDestroy");
    if (this.matiereService.title.isStopped)
      this.matiereService.title.unsubscribe();
  }
}


