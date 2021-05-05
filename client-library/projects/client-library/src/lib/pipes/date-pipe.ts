import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({ name: 'metalsoftDateFormat' })
export class MetalsoftDateFormatPipe implements PipeTransform {
  transform(date: Date): string {
    if (date === null || typeof date === 'undefined')
      return '';
    return moment.utc(date).format('DD/MM/YYYY');
  }
}
