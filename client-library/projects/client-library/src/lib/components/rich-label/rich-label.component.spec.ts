import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RichLabelComponent } from './rich-label.component';

describe('RichLabelComponent', () => {
  let component: RichLabelComponent;
  let fixture: ComponentFixture<RichLabelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RichLabelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RichLabelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
