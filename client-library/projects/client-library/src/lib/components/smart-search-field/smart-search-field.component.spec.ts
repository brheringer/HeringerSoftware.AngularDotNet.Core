import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SmartSearchFieldComponent } from './smart-search-field.component';

describe('SmartSearchFieldComponent', () => {
  let component: SmartSearchFieldComponent;
  let fixture: ComponentFixture<SmartSearchFieldComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SmartSearchFieldComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SmartSearchFieldComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
