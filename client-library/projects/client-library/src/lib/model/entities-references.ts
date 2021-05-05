import { DataTransferObject } from './data-transfer-object';
import { EntityReference } from './entity-reference';

export class EntitiesReferences extends DataTransferObject {
  references = new Array<EntityReference>();
}
