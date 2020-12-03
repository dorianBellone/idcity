import { OnInit, OnDestroy, AfterViewInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';


/**
 * Base class for all components with subscriptions which have to be unsubscribed on component's destroying
 */
export abstract class BaseDestroyableComponent implements OnDestroy, OnInit, AfterViewInit {

  private subscriptions: Subscription[] = [];


  abstract ngOnInit(): void;

  abstract ngAfterViewInit(): void;

  ngOnDestroy(): void {
    this.subscriptions.forEach((item) => item.unsubscribe());
  }

  subscribe<T>(observable: Observable<T>,
    next?: (value: T) => void,
    error?: (error: any) => void,
    complete?: () => void): void {
    this.subscriptions.push(observable.subscribe(next, error, complete));
  }
}
