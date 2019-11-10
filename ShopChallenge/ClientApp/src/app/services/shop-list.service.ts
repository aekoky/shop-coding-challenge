import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Shops } from '../enums/shops.enum';

@Injectable({
  providedIn: 'root'
})
export class ShopListService {
  private readonly _shopsListState: BehaviorSubject<Shops>;

  public get shopsListState(): Observable<Shops> {
    return this._shopsListState;
  }
  constructor() {
    this._shopsListState = new BehaviorSubject<Shops>(Shops.AllShops);
  }

  public setShopsListState(shopsState: Shops) {
    this._shopsListState.next(shopsState);
  }
}
