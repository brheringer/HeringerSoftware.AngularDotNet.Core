import { DataTransferObject } from './data-transfer-object';

export class PasswordChange extends DataTransferObject {
    currentPassword: string;
    newPassword: string;
    confirmNewPassword: string;
}
