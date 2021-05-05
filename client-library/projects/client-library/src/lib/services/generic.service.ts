import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { DataTransferObject } from '../model/data-transfer-object';
import { EntitiesReferences } from '../model/entities-references';
import { MetalsoftResponse } from '../model/metalsoft-response';
import { SessionService } from '../local-services/session.service';
//import { environment } from '../../../../../src/environments/environment'; //TODO isso nao ta bom, tem que dar um jeito de injetar

@Injectable()
export class GenericService {

  //private baseUrl: string = environment.serverUrl;
  private baseUrl;

  constructor(
    private http: HttpClient, 
    private session: SessionService,
    @Inject('environmentProvider') private environmentProvider) 
  {
    this.baseUrl = environmentProvider.serverUrl;
  }
  
  get<T>(url: string, id: number = 0): Observable<T>
  {
    let options = { headers: this.getHeaders(), withCredentials: true };
    return id > 0
     ? this.http.get<T>(this.getParameterizedFullUrl(url, id.toString()), options)
     : this.http.get<T>(this.getFullUrl(url), options);
  }

  getAll<T>(url: string): Observable<T> {
    let options = { headers: this.getHeaders(), withCredentials: true };
    return this.http.get<T>(this.getFullUrl(url), options);
  }

  post<T>(url: string, dto: any): Observable<T>
  {
    console.log("temp post " + this.getFullUrl(url));

    //let options = { headers: this.getHeaders(), withCredentials: true };
    let options = { headers: this.getHeaders() };
    return this.http.post<T>(this.getFullUrl(url), dto, options);
  }

  delete<T>(url: string, id: number): Observable<T> {
    let options = { headers: this.getHeaders(), withCredentials: true };
    return this.http.delete<T>(this.getParameterizedFullUrl(url, id.toString()), options);
  }

  smartSearch(targetService: string, smartEntry: string): Observable<EntitiesReferences> {
    let url = targetService + '/smartSearch';
    return this.post<EntitiesReferences>(url, { 'smartEntry': smartEntry } );
  }

  getText(url: string): Observable<string>
  {
    let headers = this.getHeaders()
    headers.append('responseType', 'text');
    let options = { headers: headers };
    return this.http.get<string>(this.getFullUrl(url), options );
  }

  getHeaders(): HttpHeaders
  {
    //return new HttpHeaders().set('Authorization', this.getToken());
    return new HttpHeaders().set('Authorization', 'Bearer ' + this.getToken());
  }

  private getToken(): string
  {
    if (this.session.hasCurrentSession()) {
      let currentSession = this.session.getCurrentSession();
      //return JSON.stringify(currentSession); //assim eh nos casos que integramos com o telos
      return currentSession.userSessionToken; //assim eh .net core (riscos, jwt)...
    }
    else {
      return '';
    }
  }

  getFullUrl(url: string)
  {
    return this.baseUrl + url; //TODO mais roubstez
  }

  getParameterizedFullUrl(url: string, parameter: string) {
    return this.baseUrl + url + '/' + this.clear(parameter); //TODO mais roubstez
  }

  clear(parameter: string) {
    return parameter.replace(/\//g, "%2F"); //TODO deve ter um jeito melhor de fazer, talvez com HttpUrlEncodingCodec
  }
}

