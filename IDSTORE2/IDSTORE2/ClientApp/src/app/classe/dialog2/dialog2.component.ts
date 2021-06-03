import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material';
@Component({
  selector: 'app-dialog2',
  templateUrl: './dialog2.component.html',
  styleUrls: ['./dialog2.component.css']
})
export class Dialog2Component {

  constructor(@Inject(MAT_DIALOG_DATA) public data: any) { }

  test(name) {
    console.log(name);
  }

  

}
