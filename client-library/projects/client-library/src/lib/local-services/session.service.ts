import { Injectable } from '@angular/core';
import { Observable ,  Subject } from 'rxjs';
import { UserSession } from '../model/user-session';

@Injectable()
export class SessionService {

  private readonly keyCurrentSession = 'metalsoft-absenteismoweb-currentSession';
  private sessionSubject = new Subject<UserSession>();
  //private sessionObserver: Observable<UserSession>;

  setCurrentSession(sessionDto: UserSession): void
  {
    localStorage.setItem(this.keyCurrentSession, JSON.stringify(sessionDto));
    this.sessionSubject.next(sessionDto);
  }

  hasCurrentSession(): boolean {
    return localStorage.getItem(this.keyCurrentSession) !== null;
  }

  getCurrentSession(): UserSession
  {
    return JSON.parse(localStorage.getItem(this.keyCurrentSession));
  }

  clear(): void
  {
    localStorage.removeItem(this.keyCurrentSession);
    this.sessionSubject.next(null);
  }

  getSessionSubject() {
    return this.sessionSubject;
  }
}
