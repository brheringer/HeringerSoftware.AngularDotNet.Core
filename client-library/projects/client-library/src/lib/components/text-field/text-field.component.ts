import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'metalsoft-text-field',
  templateUrl: './text-field.component.html',
  styleUrls: ['../../css/metalsoft-core.css', './text-field.component.css']
})
export class TextFieldComponent {
  @Input() text: string;
  @Output() textChange = new EventEmitter<string>();
  @Input() label: string;
  @Input() tip: string;
  @Input() required: boolean = false;
  @Input() disabled: boolean = false;

  changeText(newValue): void {
    this.text = newValue;
    this.textChange.emit(this.text);
  }
}

//ref: https://angular.io/guide/component-interaction
