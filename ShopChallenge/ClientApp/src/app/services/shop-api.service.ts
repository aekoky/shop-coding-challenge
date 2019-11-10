import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ShopModel } from '../models/shop.model';
import { Api } from './api';
import { PageModel } from '../models/page.model';

@Injectable({
  providedIn: 'root'
})
export class ShopApiService extends Api {
  protected _serviceName: string = "shop";
  _endPointsNames = {
    getShops: "shops",
    getShopsByDistance: "shopsByDistance",
    getLikedShops: "shops/liked"
  };

  constructor(private readonly httpClient: HttpClient) {
    super();
  }

  getShops(page: number, pageSize: number): Promise<PageModel<ShopModel>> {
    return new Promise((resolve, reject) => {
      try {
        const actionURL = this.getUrl(this._endPointsNames.getShops, undefined, { page, pageSize });
        this.httpClient.get<PageModel<ShopModel>>(actionURL).subscribe(
          resolve, reject
        );
      } catch (ex) {
        reject(ex);
      }
    });
  }

  getShopsByDistance(page: number, pageSize: number): Promise<PageModel<ShopModel>> {
    return new Promise((resolve, reject) => {
      try {
        const actionURL = this.getUrl(this._endPointsNames.getShopsByDistance, undefined, { page, pageSize });
        this.httpClient.get<PageModel<ShopModel>>(actionURL)
          .subscribe(
            resolve, reject
          );
      } catch (ex) {
        reject(ex);
      }
    });
  }

  getLikedShops(page: number, pageSize: number): Promise<PageModel<ShopModel>> {
    return new Promise((resolve, reject) => {
      try {
        const actionURL = this.getUrl(this._endPointsNames.getLikedShops, undefined, { page, pageSize });
        this.httpClient.get<PageModel<ShopModel>>(actionURL)
          .subscribe(
            resolve, reject
          );
      } catch (ex) {
        reject(ex);
      }
    });
  }
}
