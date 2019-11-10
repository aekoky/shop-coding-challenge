import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { Observable, of, from, Subject, Subscription } from 'rxjs';
import { ShopModel } from 'src/app/models/shop.model';
import { ShopApiService } from 'src/app/services/shop-api.service';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { ShopListService } from 'src/app/services/shop-list.service';
import { Shops } from 'src/app/enums/shops.enum';
import { PageModel } from 'src/app/models/page.model';
import { UtilitiesService } from 'src/app/services/utilities.service';

@Component({
  selector: 'app-shops-list',
  templateUrl: './shops-list.component.html',
  styleUrls: ['./shops-list.component.scss']
})
export class ShopsListComponent implements OnInit, OnDestroy {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  private readonly _subscriptions: Subscription;
  private readonly _shopSubject: Subject<ShopModel>;
  private readonly _fetchingObservable: Observable<boolean>;
  private _shopsObservable: Observable<Array<ShopModel>>;
  private _shopsState: Shops;
  private _shops: Array<ShopModel>;
  private _fetching: boolean;
  private _page: number;
  private _pageSize: number;

  public get shops(): Observable<Array<ShopModel>> {
    return this._shopsObservable;
  }

  public get fetching(): Observable<boolean> {
    return this._fetchingObservable;
  }

  constructor(
    private readonly _utilitiesService: UtilitiesService,
    private readonly _shopApiService: ShopApiService,
    private readonly _shopListService: ShopListService
  ) {
    this._subscriptions = new Subscription();
    this._shopSubject = new Subject<ShopModel>();
    this._fetchingObservable = of(this._fetching);
    this.initializeShopsList();
    this._pageSize = 9;
  }

  ngOnInit() {
    this._fetching = true;
    const shopSubscription = this._shopSubject.subscribe(shop => setTimeout(() => this._shops.push(shop)));
    this._subscriptions.add(shopSubscription);
    const shopsStateSubscription = this._shopListService.shopsListState.subscribe(
      shopsState => {
        this._shopsState = shopsState;
        this.initializeShopsList();
        this.getShops();
      }
    );
    this._subscriptions.add(shopsStateSubscription);
  }

  ngOnDestroy(): void {
    if (this._subscriptions)
      this._subscriptions.unsubscribe();
  }

  public onScroll(): void {
    this._page++;
    this.getShops();
  }

  public trackById(index: number, shop: ShopModel): number {
    return shop.id;
  }

  public removeShopFromList(shop: any) {
    const index = this._shops.indexOf(shop);
    if (index > -1) {
      this._shops.splice(index, 1);
    }
  }

  private getShops() {
    let shopsListPromise: Promise<PageModel<ShopModel>>;
    switch (this._shopsState) {
      case Shops.AllShops:
        shopsListPromise = this._shopApiService.getShops(this._page, this._pageSize);
        break;
      case Shops.PreferredShops:
        shopsListPromise = this._shopApiService.getLikedShops(this._page, this._pageSize);
        break;
      case Shops.NearbyShops:
        shopsListPromise = this._shopApiService.getShopsByDistance(this._page, this._pageSize);
        break;
    }

    shopsListPromise.then(async (page) => {
      if (page)
        await this.loadShops(page.data);
    })
      .catch((err) => {
        debugger;
        if (err.ds)
          this._utilitiesService.notify("error while loading shops list");

      });
  }

  private initializeShopsList() {
    this._shops = new Array<ShopModel>();
    this._page = 0;
    this._shopsObservable = of(this._shops);
  }

  private async loadShops(shops: ReadonlyArray<ShopModel>) {
    for await (let shop of shops)
      this._shopSubject.next(shop);
  }
}
