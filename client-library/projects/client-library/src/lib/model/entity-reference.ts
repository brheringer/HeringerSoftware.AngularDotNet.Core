import { DataTransferObject } from './data-transfer-object';

export class EntityReference extends DataTransferObject {
  id: number = 0;
  presentation: string = '';
  version: number = 0;
}
