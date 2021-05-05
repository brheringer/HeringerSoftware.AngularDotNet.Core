import { Collection } from './collection';
import { UserProfile } from './user-profile.model';

export class UsersProfiles extends Collection {
  filterName: string;
  items: Array<UserProfile>;
}
