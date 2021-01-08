import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { BaseDestroyableComponent } from '../common/baseOnInit.component';
import { Fichier } from '../models/fichier';
import { ApiService } from '../services/api.service';
import { MatiereService } from '../services/matiere.service';

@Component({
  selector: 'app-liste',
  templateUrl: './liste.component.html',
  styleUrls: ['./liste.component.css']
})
export class ListeComponent extends BaseDestroyableComponent {
  mySubjectVal: string;
  matiere: String;
  data: Fichier[];
  da: any;
  blob: Blob;
  classe: string;



  constructor(private matiereService: MatiereService, private apiService: ApiService) {
    super();
  }
  ngAfterViewInit(): void {
  }

  ngOnInit(): void {
    this.classe = "";
    this.mySubjectVal = "";
    this.matiereService.title.subscribe(
      data => {
        this.mySubjectVal = data;
      });
    this.apiService.title.subscribe(
      data => {
        this.classe = data;
      });
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


