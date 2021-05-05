import { DataTransferObject } from './data-transfer-object';

export class Collection extends DataTransferObject {
  searchMaxResults: number;
  searchPage: number;
}
