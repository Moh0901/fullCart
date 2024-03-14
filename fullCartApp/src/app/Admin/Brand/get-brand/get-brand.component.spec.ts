import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetBrandComponent } from './get-brand.component';

describe('GetBrandComponent', () => {
  let component: GetBrandComponent;
  let fixture: ComponentFixture<GetBrandComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GetBrandComponent]
    });
    fixture = TestBed.createComponent(GetBrandComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
