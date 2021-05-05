import { Component, EventEmitter, Input, Output } from '@angular/core';
import { EntityReference } from '../../model/entity-reference';

@Component({
  selector: 'metalsoft-select-primitive-field',
  templateUrl: './select-primitive-field.component.html',
  styleUrls: ['../../css/metalsoft-core.css', './select-primitive-field.component.css']
})
export class SelectPrimitiveFieldComponent {
  @Input() item: any;
  @Output() itemChange = new EventEmitter<any>();
  @Input() items: Array<any>;
  @Input() label: string;
  @Input() tip: string;
  @Input() required: boolean = false;
  @Input() disabled: boolean = false;

  changeItem(): void {
    this.itemChange.emit(this.item);
  }
}

//ref: https://angular.io/guide/component-interaction
