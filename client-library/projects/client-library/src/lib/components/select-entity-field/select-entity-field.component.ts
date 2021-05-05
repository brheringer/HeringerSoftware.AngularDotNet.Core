import { Component, EventEmitter, Input, Output } from '@angular/core';
import { EntityReference } from '../../model/entity-reference';

@Component({
  selector: 'metalsoft-select-entity-field',
  templateUrl: './select-entity-field.component.html',
  styleUrls: ['../../css/metalsoft-core.css', './select-entity-field.component.css']
})
export class SelectEntityFieldComponent {
  @Input() item: EntityReference;
  @Output() itemChange = new EventEmitter<EntityReference>();
  @Input() items: Array<EntityReference>;
  @Input() label: string;
  @Input() tip: string;
  @Input() required: boolean = false;
  @Input() disabled: boolean = false;

  changeItem(): void {
    this.itemChange.emit(this.item);
  }

  compareEntities(a, b): boolean {
    return a && b && a.id === b.id;
  }
}

//ref: https://angular.io/guide/component-interaction
