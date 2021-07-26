import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SincronizationsComponent } from './sincronizations.component';

describe('SincronizationsComponent', () => {
  let component: SincronizationsComponent;
  let fixture: ComponentFixture<SincronizationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SincronizationsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SincronizationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
