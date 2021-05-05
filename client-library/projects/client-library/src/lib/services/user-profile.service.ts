import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { GenericService } from './generic.service';
import { SessionService } from '../local-services/session.service';

import { UserProfile } from '../model/user-profile.model';
import { UsersProfiles } from '../model/users-profiles.model';
import { PasswordChange } from '../model/password-change';

@Injectable()
export class UserProfileService {

  private uri: string = 'userProfile';

  constructor(private generic: GenericService, private session: SessionService) { }

  get(id: number): Observable<UserProfile> {
    return this.generic.get<UserProfile>(this.uri, id);
  }

  delete(id: number): Observable<UserProfile> {
    return this.generic.delete<UserProfile>(this.uri, id);
  }

  search(dto: UsersProfiles): Observable<UsersProfiles> {
    return this.generic.post<UsersProfiles>(`${this.uri}/search`, dto);
  }

  update(dto: UserProfile): Observable<UserProfile> {
    return this.generic.post<UserProfile>(this.uri, dto);
  }
}
