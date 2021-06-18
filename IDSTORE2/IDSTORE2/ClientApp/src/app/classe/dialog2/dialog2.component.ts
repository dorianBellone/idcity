import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material';
import { FileUploadService } from '../../services/file-upload-service';
@Component({
  selector: 'app-dialog2',
  templateUrl: './dialog2.component.html',
  styleUrls: ['./dialog2.component.css']
})
export class Dialog2Component {
  private fileToUpload: File = null;
  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private fileUploadService: FileUploadService) { }

  test(name) {
    console.log(name);
  }

  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
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
