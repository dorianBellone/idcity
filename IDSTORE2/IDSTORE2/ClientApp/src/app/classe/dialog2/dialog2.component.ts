import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material';
import { FileUploadService } from '../../services/file-upload-service';
import { Output, EventEmitter } from '@angular/core';
import { HttpEventType, HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-dialog2',
  templateUrl: './dialog2.component.html',
  styleUrls: ['./dialog2.component.css']
})
export class Dialog2Component {
  private fileToUpload: File = null;
  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private fileUploadService: FileUploadService, private http: HttpClient) { }

  test(name) {
    console.log(name);
  }

  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
  }

  public progress: number;
  public message: string;
  @Output() public onUploadFinished = new EventEmitter();

  public UpdateFile = (name,files) => {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    //update / { classe } / { name } / { newname }
    this.http.post('https://localhost:44373/file/update/' + name, formData, { reportProgress: true, observe: 'events' })
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.message = 'Upload success.';
          this.onUploadFinished.emit(event.body);
        }
      });
  }
   
  public testUpload(name, files)
  {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.http.post('https://localhost:44373/file/upload', formData, { reportProgress: true, observe: 'events' })
      .subscribe(event => {
          this.message = 'Upload success.';
          //this.onUploadFinished.emit(event.body);
        });
  }



  uploadFileToActivity() {


    this.fileUploadService.postFile(this.fileToUpload).subscribe(
      data => {
        console.log(data);
      },
      (err) => {
        console.log("Upload Error:", err);
      }, () => {
        console.log("Upload done");
      }
    )
  }
}
