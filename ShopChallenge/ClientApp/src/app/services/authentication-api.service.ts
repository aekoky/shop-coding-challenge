import { Injectable } from '@angular/core';
import { Api } from './api';
import { UserModel } from '../models/user.model';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';

export const TOKEN_STORAGE_NAME = "identity";
export const EMAIL_STORAGE_NAME = "user_email";

@Injectable({
  providedIn: 'root'
})
export class AuthenticationApiService extends Api {
  private readonly _user: BehaviorSubject<UserModel>;
  private readonly _userState: BehaviorSubject<boolean>;
  protected readonly _serviceName: string = "user";
  private _currentUser: UserModel;
  _endPointsNames = {
    login: "login"
  };

  public get user(): Observable<UserModel> {
    return this._user;
  }

  public get userState(): Observable<boolean> {
    return this._userState;
  }

  public get apiService(): string {
    return this._server;
  }

  constructor(private readonly httpClient: HttpClient) {
    super();
    this._currentUser = this.loadUser();
    this._user = new BehaviorSubject<UserModel>(this._currentUser);
    this._userState = new BehaviorSubject<boolean>(false);
    this._user.subscribe(
      user => this.notifyUserState(user)
    );
    this._user.next(this._currentUser);
  }

  authenticateUser(userModel: UserModel): Observable<UserModel> {
    try {
      let apiUrl = this.getUrl(this._endPointsNames.login);
      return this.httpClient
        .post<UserModel>(
          apiUrl,
          userModel,
          {
            headers: this._headers,
            observe: 'response'
          })
        .pipe(
          map(resp => resp.body),
          map(user => {
            if (user && user.token) {
              userModel = user;
              localStorage.setItem(TOKEN_STORAGE_NAME, user.token);
              localStorage.setItem(EMAIL_STORAGE_NAME, user.email);
              this._user.next(user);
              this._currentUser = user;
            }
            return user;
          })
        );
    } catch (err) {
      catchError(err);
    }
  }

  logOut(): void {
    localStorage.removeItem(TOKEN_STORAGE_NAME);
    localStorage.removeItem(EMAIL_STORAGE_NAME);
    this._currentUser = undefined;
    this._user.next(this._currentUser);
  }

  private notifyUserState(userModel?: UserModel) {
    const userState: boolean = !!this.loadUser() || !!userModel;
    this._userState.next(userState);
  }

  private loadUser(): UserModel {
    const token = localStorage.getItem(TOKEN_STORAGE_NAME);
    const email = localStorage.getItem(EMAIL_STORAGE_NAME);
    if (token && email) {
      return <UserModel>{ email, token };
    }
    return undefined;
  }
}
