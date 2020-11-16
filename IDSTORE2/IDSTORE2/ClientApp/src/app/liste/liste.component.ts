import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatiereService } from '../services/matiere.service';


@Component({
  selector: 'app-liste',
  templateUrl: './liste.component.html',
  styleUrls: ['./liste.component.css']
})
export class ListeComponent implements OnInit {
  mySubjectVal: string;
  @Input('myInputVal') myInputVal: string;
  @Output('myOutputVal') myOutputVal = new EventEmitter();
  matiere: String;

  constructor(private matiereService: MatiereService) { }

  ngOnInit(): void {
    this.matiereService.title.subscribe(
      data => {
        this.mySubjectVal = data;
        console.log(data);
      }
    );

  }

  ngOnDestroy() {
    console.log("inside child component ngOnDestroy");
    if (this.matiereService.title.isStopped)
      this.matiereService.title.unsubscribe();
  }
}


