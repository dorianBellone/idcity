import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogUpdateCoursComponent } from './dialog-update-cours.component';

describe('DialogUpdateCoursComponent', () => {
  let component: DialogUpdateCoursComponent;
  let fixture: ComponentFixture<DialogUpdateCoursComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [DialogUpdateCoursComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogUpdateCoursComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
