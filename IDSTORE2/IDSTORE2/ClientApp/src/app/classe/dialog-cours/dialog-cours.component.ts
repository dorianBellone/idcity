import { Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { HttpEventType, HttpClient } from '@angular/common/http';
import { MAT_DIALOG_DATA } from '@angular/material';

interface Classe {
  nom: string;
}

@Component({
  selector: 'app-dialog-cours',
  templateUrl: './dialog-cours.component.html',
  styleUrls: ['./dialog-cours.component.css']
})


export class DialogCoursComponent implements OnInit {
  @Output() public onUploadFinished = new EventEmitter();

  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private http: HttpClient) { }

  public progress: number;
  public message: string;

  ngOnInit() {
  }

  classes: Classe[] = [
    { nom: 'B1' },
    { nom: 'B2' },
    { nom: 'B3' },
    { nom: 'M1' },
    { nom: 'M2' }
  ];

  

  public AddCours = (name, files) => {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.http.post('https://localhost:44373/file/upload/' + name, formData, { reportProgress: true, observe: 'events' })
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.message = 'Upload success.';
          this.onUploadFinished.emit(event.body);
        }
      });
  }
  


}
