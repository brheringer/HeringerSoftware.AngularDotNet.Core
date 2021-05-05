import { Entity } from './entity';
import { UserProfileAssignment } from './user-profile-assignment.model';

export class User extends Entity {
    realName: string;
    userName: string;
    password: string;
    newPassword: string;
    banished: boolean;
    banishedReason: string;
    email: string;
    profilesAssignments: Array<UserProfileAssignment> = new Array<UserProfileAssignment>();
}
