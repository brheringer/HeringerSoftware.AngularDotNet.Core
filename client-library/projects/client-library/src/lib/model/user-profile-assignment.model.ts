import { Entity } from './entity';
import { EntityReference } from './entity-reference';

export class UserProfileAssignment extends Entity {
  assignedProfile: EntityReference;
}
