import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserModule } from './user/user.module';
import { ShopModule } from './shop/shop.module';
import {  MatSnackBarModule } from '@angular/material/snack-bar';

const modules=[
  CommonModule,
  MatSnackBarModule,
  UserModule,
  ShopModule
]


const components=[
]

@NgModule({
  imports: [
    ...modules
  ],
  declarations: [
    ...components
  ],
  exports:[
    ...modules
  ]
})
export class ShopAppModule { }
