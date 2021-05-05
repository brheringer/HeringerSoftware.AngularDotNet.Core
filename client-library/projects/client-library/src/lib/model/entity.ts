import { DataTransferObject } from './data-transfer-object';

export class Entity extends DataTransferObject {
  id: number = 0;
  presentation: string = '';
  version: number = 0;
  creationDateTime: Date;
  creationUser: string = '';
  lastUpdateDateTime: Date;
  lastUpdateUser: string = '';
  deleteMe: boolean = false;

  public isPersistent(): boolean
  {
    return this.id > 0;
  }
}
