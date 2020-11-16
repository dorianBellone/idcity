import { Injectable, OnInit } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class MatiereService {
  public title = new Subject<string>();;


  loadMatiere(data) {

    this.title.next(data);

  }
}
