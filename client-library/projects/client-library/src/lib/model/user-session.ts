import { DataTransferObject } from './data-transfer-object';

export class UserSession extends DataTransferObject {
  userSessionToken: string;
  userName: string;
}
