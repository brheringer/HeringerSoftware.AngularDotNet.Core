import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'metalsoft-rich-label',
  templateUrl: './rich-label.component.html',
  styleUrls: ['../../css/metalsoft-core.css', './rich-label.component.css']
})
export class RichLabelComponent {
  @Input() label: string;
  @Input() tip: string;
  @Input() icon: string; //text, number, date, select, smart-search, boolean

  getIconUrl() {
    return `assets/${this.icon}_24px.svg`; //TODO rever
  }
}
