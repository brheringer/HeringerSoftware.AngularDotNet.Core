import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'metalsoftBooleanFormat' })
export class MetalsoftBooleanFormatPipe implements PipeTransform {
  transform(flag: boolean): string {
    return flag ? 'Sim' : 'Não';
  }
}
