import { Injectable, OnInit } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class ClasseService {
  public title = new Subject<string>();;


  loadClasse(data) {

    this.title.next(data);
  this.title.subscribe(
      value => console
        .log(value)
    );
  }
}
