import { Injectable } from '@angular/core';
import { UserModel } from '../models/user.model';
import { ShopModel } from '../models/shop.model';
import { Api } from './api';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserApiService extends Api {

  protected _serviceName: string = "user";

  _endPointsNames = {
    users: "users",
    like: "like",
    dislike: "dislike"
  };

  constructor(private readonly httpClient: HttpClient) {
    super();
  }

  signUpUser(userModel: UserModel): Promise<UserModel> {
    debugger;
    return new Promise((resolve, reject) => {
      try {
        const actionURL = this.getUrl(this._endPointsNames.users);
        this.httpClient.post<UserModel>(actionURL, userModel)
          .subscribe(
            resolve, reject
          );
      } catch (ex) {
        reject(ex);
      }
    });
  }

  likeShop(shopModel: ShopModel): Promise<ShopModel> {
    return new Promise((resolve, reject) => {
      try {
        const actionURL = this.getUrl(this._endPointsNames.like, [shopModel.id.toString()]);
        this.httpClient.post<ShopModel>(actionURL, undefined)
          .subscribe(
            resolve, reject
          );
      } catch (ex) {
        reject(ex);
      }
    });
  }

  dislikeShop(shopModel: ShopModel): Promise<ShopModel> {
    return new Promise((resolve, reject) => {
      try {
        const actionURL = this.getUrl(this._endPointsNames.dislike, [shopModel.id.toString()]);
        this.httpClient.post<ShopModel>(actionURL, undefined)
          .subscribe(
            resolve, reject
          );
      } catch (ex) {
        reject(ex);
      }
    });
  }
}
