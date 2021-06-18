import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogCoursComponent } from './dialog-cours.component';

describe('DialogCoursComponent', () => {
  let component: DialogCoursComponent;
  let fixture: ComponentFixture<DialogCoursComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DialogCoursComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogCoursComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
