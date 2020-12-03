import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgxFileSaverService } from '@clemox/ngx-file-saver';
import { BaseDestroyableComponent } from '../common/baseOnInit.component';
import { Fichier } from '../models/fichier';
import { ApiService } from '../services/api.service';
import { MatiereService } from '../services/matiere.service';
import { saveAs } from 'file-saver';

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

  constructor(private matiereService: MatiereService, private apiService: ApiService, private fileSaver: NgxFileSaverService
  ) {
    super();
  }

  ngAfterViewInit(): void {
    this.matiereService.title.subscribe(
      data => {
        this.mySubjectVal = data;
      });
  }

  ngOnInit(): void {
    this.apiService.Test()
      .subscribe(
        data => {
          this.data = data;
        }
      );
  }

  private download(): void {
    this.apiService.get();
    //this.apiService.getFile()
    //  .subscribe(
    //    (data: Blob) => {
    //      saveAs(data, `rrr.pdf`); // from file-saver library
    //    },
    //    (err: any) => {
    //      console.log(`Unable to save file ${JSON.stringify(err)}`)
    //    }
    //  );
  }


  DownloadFile(filePath: string, fileType: string): Observable<any> {
    let fileExtension = fileType;
    let input = filePath;
    return this.apiService.post("http://localhost:21021/api/services/app/FormAttachment/DownloadFile?fileName=" + input, '',
      { responseType: ResponseContentType.Blob })
      .map(
        (res) => {
          var blob = new Blob([res.blob()], { type: fileExtension })
          return blob;
        });
  }


  ngOnDestroy() {
    console.log("inside child component ngOnDestroy");
    if (this.matiereService.title.isStopped)
      this.matiereService.title.unsubscribe();
  }
}


