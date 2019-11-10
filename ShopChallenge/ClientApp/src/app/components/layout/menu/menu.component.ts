import { Component, OnInit } from '@angular/core';
import { AuthenticationApiService } from 'src/app/services/authentication-api.service';
import { Observable } from 'rxjs';
import { LayoutService } from 'src/app/services/layout.service';
import { Views } from 'src/app/enums/views.enum';
import { Menus } from 'src/app/enums/menus.enum';
import { UtilitiesService } from 'src/app/services/utilities.service';
import { distinctUntilChanged } from 'rxjs/operators';
import { ShopListService } from 'src/app/services/shop-list.service';
import { Shops } from 'src/app/enums/shops.enum';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {
  menus = Menus;
  _menuState: Observable<Menus>;

  public get menuState(): Observable<Menus> {
    return this._menuState;
  }

  constructor(
    private readonly _authenticationApiService: AuthenticationApiService,
    private readonly _layoutService: LayoutService,
    private readonly _shopsListService: ShopListService,
    private readonly _utilitiesService: UtilitiesService
  ) {
    this._menuState = _layoutService.menuState.pipe(distinctUntilChanged());
  }

  ngOnInit() {
  }

  public logOut(): void {
    this._authenticationApiService.logOut();
    this._layoutService.setViewStat(Views.SingingIn);
    this._utilitiesService.notify("Loged out");
  }

  public signUp(): void {
    this._layoutService.setViewStat(Views.SingingUp);
  }

  public signIn(): void {
    this._layoutService.setViewStat(Views.SingingIn);
  }

  public preferredShops(): void {
    this._shopsListService.setShopsListState(Shops.PreferredShops);
  }

  public nearbyShops(): void {
    this._shopsListService.setShopsListState(Shops.NearbyShops);
  }

  public allShops(): void {
    this._shopsListService.setShopsListState(Shops.AllShops);
  }
}
