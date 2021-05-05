import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'metalsoft-boolean-field',
  templateUrl: './boolean-field.component.html',
  styleUrls: ['../../css/metalsoft-core.css', './boolean-field.component.css']
})
export class BooleanFieldComponent {
  @Input() booleanValue: boolean;
  @Output() booleanValueChange = new EventEmitter<boolean>();
  @Input() label: string;
  @Input() tip: string;
  @Input() disabled: boolean = false;

  changeBooleanValue(): void {
    this.booleanValueChange.emit(this.booleanValue);
  }
}
