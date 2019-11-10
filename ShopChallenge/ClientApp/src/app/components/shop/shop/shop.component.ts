import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ShopModel } from 'src/app/models/shop.model';
import { UserApiService } from 'src/app/services/user-api.service';
import { UtilitiesService } from 'src/app/services/utilities.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @Input() shop: ShopModel;
  @Output() liked = new EventEmitter<ShopModel>();
  @Output() disliked = new EventEmitter<ShopModel>();

  constructor(private readonly userService: UserApiService,
    private readonly utilitiesService: UtilitiesService
  ) { }

  ngOnInit() {
  }

  likeShop() {
    this.userService.likeShop(this.shop)
      .then(_ => {
        this.utilitiesService.notify('Shop is liked');
        this.liked.emit(this.shop);
      })
      .catch(err => {
        this.utilitiesService.notify('Something went wrong');
      });
  }

  dislikeShop() {
    this.userService.dislikeShop(this.shop)
      .then(_ => {
        this.utilitiesService.notify('Shop is disliked');
        this.disliked.emit(this.shop);
      })
      .catch(err => {
        this.utilitiesService.notify('Something went wrong');
      });
  }
}
