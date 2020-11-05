import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeveloppementComponent } from './developpement.component';

describe('DeveloppementComponent', () => {
  let component: DeveloppementComponent;
  let fixture: ComponentFixture<DeveloppementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeveloppementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeveloppementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
