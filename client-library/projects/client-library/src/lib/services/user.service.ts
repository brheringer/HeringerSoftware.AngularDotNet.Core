import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { GenericService } from './generic.service';
import { SessionService } from '../local-services/session.service';

import { User } from '../model/user';
import { Users } from '../model/users';
import { PasswordChange } from '../model/password-change';

@Injectable()
export class UserService {

  private uri: string = 'user';

  constructor(private generic: GenericService, private session: SessionService) { }

  get(id: number): Observable<User> {
    return this.generic.get<User>(this.uri, id);
  }

  delete(id: number): Observable<User> {
    return this.generic.delete<User>(this.uri, id);
  }

  search(dto: Users): Observable<Users> {
    return this.generic.post<Users>(`${this.uri}/search`, dto);
  }

  update(dto: User): Observable<User> {
    return this.generic.post<User>(this.uri, dto);
  }
}
