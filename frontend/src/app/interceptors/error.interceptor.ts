import { Injectable } from '@angular/core';
import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from "rxjs/operators";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor() {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          let errorMsg = '';
          if (error.error instanceof ErrorEvent) {
            errorMsg = 'Unexpected error occured. Please try later.';
          } else if(error.status === 400 
                  && error.error.errors !== null 
                  && typeof error.error.errors === 'object' ) {

            let props = Object.keys(error.error.errors);
            errorMsg = props.map(prop => error.error.errors[prop]).join(' ')
          } else if (error.status === 404) {
            errorMsg = 'Resource not found.';
          } else {
            errorMsg = 'Unexpected error occured. Please try later.';
          }
          return throwError(() => new Error(errorMsg));
        })
      )
  }
}