import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticationApiService } from './authentication-api.service';
import { UserModel } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class JwtInterceptorService implements HttpInterceptor {
  private user: UserModel;

  constructor(private readonly authenticationApiService: AuthenticationApiService) {
    this.authenticationApiService.user.subscribe(
      user => this.user = user
    );
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // add auth header with jwt if user is logged in and request is to api url
    const isApiUrl = req.url.startsWith(this.authenticationApiService.apiService);
    if (isApiUrl && this.user) {
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${this.user.token}`
        }
      });
    }

    return next.handle(req);
  }

}
