import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { GenericService } from './generic.service';
import { SessionService } from '../local-services/session.service';

import { User } from '../model/user';
import { UserSession } from '../model/user-session';
import { PasswordChange } from '../model/password-change';

@Injectable()
export class LoginService {

  constructor(
    private http: HttpClient,
    private session: SessionService,
    @Inject('environmentProvider') private environmentProvider) { }

  login(userDto: User): Observable<UserSession>
  {
    let options = { 
      headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded') 
    };

    let body = new URLSearchParams();
    body.set('grant_type', 'password');
    body.set('username', userDto.userName);
    body.set('password', userDto.password);
    body.set('client_id', this.environmentProvider.identity.clientId);
    body.set('client_secret', this.environmentProvider.identity.clientSecret);
    body.set('scope', this.environmentProvider.identity.scope);

    let observable = new Observable<UserSession>(observer => {
      this.http.post<any>(`${this.environmentProvider.identity.url}/connect/token`, body.toString(), options).subscribe(
        identityResponse => {
          let session = new UserSession();
          if (identityResponse && identityResponse.access_token) {
            session.userName = userDto.userName;
            session.userSessionToken = identityResponse.access_token;
            this.session.setCurrentSession(session);
          }
          else { //TODO rever
            session.response.hasException = true;
            session.response.exception = identityResponse; //TODO rever
          }
          observer.next(session);
          observer.complete();
        },
        err => {
          observer.error(err);
          observer.complete();
        }
      );
    });
    return observable;
  }

  logout(): void
  {
    //TODO logout no servidor?
    this.session.clear();
  }

  changePassword(dto: any) : any {
    /*TODO*/
  }
}
