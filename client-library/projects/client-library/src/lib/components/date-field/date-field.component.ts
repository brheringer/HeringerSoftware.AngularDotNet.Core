import { Component, EventEmitter, Input, Output } from '@angular/core';
import * as moment from 'moment';

@Component({
  selector: 'metalsoft-date-field',
  templateUrl: './date-field.component.html',
  styleUrls: ['../../css/metalsoft-core.css', './date-field.component.css']
})
export class DateFieldComponent {
  @Input() date: Date;
  @Output() dateChange = new EventEmitter<Date>();
  @Input() label: string;
  @Input() tip: string;
  @Input() required: boolean = false;
  @Input() disabled: boolean = false;

  changeDate(): void {
    this.dateChange.emit(this.date);
  }

  getDateYYYYMMDD(date: Date): string {
    if (date === null || typeof date === 'undefined')
      return null;
    return moment.utc(date).format('YYYY-MM-DD');
  }

  setDateYYYYMMDD(incoming: string) {
    if (incoming === null || incoming === 'undefined' || incoming == '') {
      this.date = null;
    }
    else {
      //let utc = incoming + 'T00:00:00Z';
      this.date = new Date(incoming);
    }
  }
}

//ref: https://angular.io/guide/component-interaction
