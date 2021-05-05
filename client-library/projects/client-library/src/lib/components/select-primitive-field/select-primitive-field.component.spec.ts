import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectPrimitiveFieldComponent } from './select-primitive-field.component';

describe('SelectPrimitiveFieldComponent', () => {
  let component: SelectPrimitiveFieldComponent;
  let fixture: ComponentFixture<SelectPrimitiveFieldComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SelectPrimitiveFieldComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectPrimitiveFieldComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
