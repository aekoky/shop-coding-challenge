import { Injectable } from '@angular/core';
import { Observable, Subject, BehaviorSubject } from 'rxjs';
import { Views } from '../enums/views.enum';
import { AuthenticationApiService } from './authentication-api.service';
import { Menus } from '../enums/menus.enum';

@Injectable({
  providedIn: 'root'
})
export class LayoutService {
  private readonly _viewState: BehaviorSubject<Views>;
  private readonly _menuState: BehaviorSubject<Menus>;
  private readonly _userState: Observable<boolean>;
  private _currentUserStat: boolean;

  public get viewState(): Observable<Views> {
    return this._viewState;
  }

  public get menuState(): Observable<Menus> {
    return this._menuState;
  }

  constructor(_authenticationApiService: AuthenticationApiService) {
    this._viewState = new BehaviorSubject<Views>(Views.SingingIn);
    this._menuState = new BehaviorSubject<Menus>(Menus.Visitor);
    this._userState = _authenticationApiService.userState;
    this._userState.subscribe(userState => {
      this._currentUserStat = userState;
      this._menuState.next(this._currentUserStat ? Menus.User : Menus.Visitor);
      this._viewState.next(this._currentUserStat ? Views.Shops : Views.SingingIn);
    });
  }

  public setViewStat(view: Views): void {
    if (view === Views.Shops && !this._currentUserStat) {
      this._viewState.next(Views.SingingIn);

      return;
    }
    this._menuState.next(this._currentUserStat ? Menus.User : Menus.Visitor);
    this._viewState.next(view);
  }
}
