
import {catchError, tap} from 'rxjs/operators';

import {throwError as observableThrowError,  Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpEvent, HttpResponse, HttpErrorResponse, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { EmptyObservable } from 'rxjs/observable/EmptyObservable';



import { DataTransferObject } from '../model/data-transfer-object';
import { MetalsoftResponse } from '../model/metalsoft-response';
import { AlertService } from '../local-services/alert.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable()
export class MetalsoftHttpInterceptor implements HttpInterceptor {

  constructor(private alertService: AlertService, private spinnerService : NgxSpinnerService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    //return next.handle(req);
    return next
      .handle(req).pipe(
      tap(event => {
        this.alertIfException(event);
        return event;
      }),
      catchError(err => {
        if (err instanceof HttpErrorResponse) {
          let dto = this.dealWithLowLevelError(err);
          this.log(dto.response.exception);
        }
        return observableThrowError(err);
      }),);

    //if (error instanceof HttpErrorResponse) {
    //  if (error.status === 401) {
    //    authService.logout();
    //  }
    //}
  }

  alertIfException(event: HttpEvent<any>): void {
    if(event instanceof HttpResponse)
      if(event.body.response && event.body.response.hasException)
        this.log(event.body.response.exception);
  }

  dealWithLowLevelError(err: HttpErrorResponse): DataTransferObject {
    let genericErrorMessageDto = new DataTransferObject();
    genericErrorMessageDto.response = new MetalsoftResponse();
    genericErrorMessageDto.response.hasException = true;

    if (err.error instanceof Error) {
      // A client-side or network error occurred. Handle it accordingly.
      this.log(err.error.message);
      genericErrorMessageDto.response.exception = "Uma operação não foi concluída com sucesso. Consulte o log.";
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      this.log('[' + err.status + '] ' + err.message);
      genericErrorMessageDto.response.exception = "[${err.status}] Uma operação não foi concluída com sucesso. Consulte o log.";
    }
    return genericErrorMessageDto;
  }

  log(msg): void {
    this.alertService.error('interceptor: ' + msg); //TODO nao gostei de usar isso aqui mas nao consegui fazer de outro jeito
    this.spinnerService.hide(); //TODO nao gostei de usar isso aqui mas nao consegui parar de outro jeito quando API nao esta rodando
    console.log(msg);
  }
}

/*
The err parameter to the callback above is of type HttpErrorResponse, and contains useful information on what went wrong.
There are two types of errors that can occur. If the backend returns an unsuccessful response code (404, 500, etc.), it gets returned as an error. Also, if something goes wrong client-side, such as an exception gets thrown in an RxJS operator, or if a network error prevents the request from completing successfully, an actual Error will be thrown.
In both cases, you can look at the HttpErrorResponse to figure out what happened.
*/
