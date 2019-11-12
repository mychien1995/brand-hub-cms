import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { AuthenticationService } from '../services/authentication/authentication.service';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable()

export class AuthenticationInterceptor implements HttpInterceptor {
  constructor(private auth: AuthenticationService, private router : Router) {

  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    
    return next.handle(request).pipe(
      tap( event => {}, error => {
        if (error instanceof HttpErrorResponse) {
        if (error.status === 401) {
          alert('You are not logged in');
           this.router.navigate(['/login']);
        }
      }
    })
    );
  }
}