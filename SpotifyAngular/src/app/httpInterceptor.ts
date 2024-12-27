import { Injectable } from '@angular/core';
import {
    HttpEvent,
    HTTP_INTERCEPTORS,
    HttpInterceptor,
    HttpHandler,
    HttpRequest,
    HttpErrorResponse,
  } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError,switchMap } from 'rxjs/operators';
import { AuthenticationService } from './Services/authentication.service'
import { GlobalVariables } from './Global';
import { RefreshTokenRequestModel } from './Models/refreshTokenRequestModel';

  @Injectable()
  export class httpInterceptor implements HttpInterceptor { 
    constructor(private auth: AuthenticationService) {}
    
    intercept(httpRequest: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {       
        if (GlobalVariables.AUTH_TOKEN!='') {
            // If we have a token, we set it to the header          
            httpRequest = httpRequest.clone({
               setHeaders: {Authorization: `Bearer ${GlobalVariables.AUTH_TOKEN}`}
            });            
        }     
       
      return next.handle(httpRequest).pipe(catchError((err:HttpErrorResponse)=>{      
       if (err.status === 401 && GlobalVariables.REFRESH_TOKEN!='') {
            const refreshReq:RefreshTokenRequestModel={
              userName:GlobalVariables.USER_NAME,
              refreshToken:GlobalVariables.REFRESH_TOKEN
            };
            const newAccessToken = this.auth.RefreshToken(refreshReq);
            const clonedRequest = httpRequest.clone({
              setHeaders: {
                Authorization: `Bearer ${newAccessToken}`
              }
            });
            return next.handle(clonedRequest);         
        }
        console.log(err);     
        return throwError(err);
      }));
      
      return next.handle(httpRequest);   
    }
  }