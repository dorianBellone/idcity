import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material';
import { Output, EventEmitter } from '@angular/core';
import { HttpEventType, HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-dialog-update-cours',
  templateUrl: './dialog-update-cours.component.html',
  styleUrls: ['./dialog-update-cours.component.css']
})
export class DialogUpdateCoursComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private http: HttpClient) { }

  test(name) {
    console.log(name);
  }
  

  public progress: number;
  public message: string;
  public formData: FormData;

  @Output() public onUploadFinished = new EventEmitter();

  public UpdateFileTwo = (name) => {
    //update / { classe } / { name } / { newname }
    this.http.post('https://localhost:44373/file/update/B1/' + name + "/aa", this.formData, { reportProgress: true, observe: 'events' })
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.message = 'Upload success.';
          this.onUploadFinished.emit(event.body);
        }
      });
  }

  public UpdateFile = (name,files) => {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    //update / { classe } / { name } / { newname }
    this.http.post('https://localhost:44373/file/update/B1' + name + "/aa", formData, { reportProgress: true, observe: 'events' })
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.message = 'Upload success.';
          this.onUploadFinished.emit(event.body);
        }
      });
  }
  public OnChangeFile = (files) => {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    this.formData = new FormData();
    this.formData.append('file', fileToUpload, fileToUpload.name);
   
  }
}
