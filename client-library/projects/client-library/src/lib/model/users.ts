import { Collection } from './collection';
import { User } from './user';

export class Users extends Collection {
  filterRealName: string;
  filterUserName: string;
  items: Array<User>;
}
