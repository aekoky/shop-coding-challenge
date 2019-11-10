import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop/shop.component';
import { ShopsListComponent } from './shops-list/shops-list.component';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatPaginatorModule } from '@angular/material/paginator';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { NgxSkeletonLoaderModule } from 'ngx-skeleton-loader';

const components = [
  ShopComponent,
  ShopsListComponent
];

const modules = [
  CommonModule,
  MatGridListModule,
  MatCardModule,
  MatIconModule,
  MatButtonModule,
  MatPaginatorModule,
  InfiniteScrollModule,
  NgxSkeletonLoaderModule
];

@NgModule({
  declarations: [
    ...components
  ],
  imports: [
    ...modules
  ],
  exports: [
    ...components
  ]
})
export class ShopModule { }
