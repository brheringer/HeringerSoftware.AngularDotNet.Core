import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'metalsoft-number-field',
  templateUrl: './number-field.component.html',
  styleUrls: ['../../css/metalsoft-core.css', './number-field.component.css']
})
export class NumberFieldComponent {
  @Input() model: number;
  @Output() modelChange = new EventEmitter<number>();
  @Input() label: string;
  @Input() tip: string;
  @Input() required: boolean = false;
  @Input() disabled: boolean = false;

  changeNumber(): void {
    this.modelChange.emit(this.model);
  }
}

//ref: https://angular.io/guide/component-interaction
