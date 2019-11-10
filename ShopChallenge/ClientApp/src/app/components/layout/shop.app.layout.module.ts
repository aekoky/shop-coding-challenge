import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuComponent } from './menu/menu.component';
import { MatButtonModule } from '@angular/material/button';
import { HttpClientModule } from '@angular/common/http';
import { BodyComponent } from './body/body.component';
import { ShopAppModule } from '../shop.app.module';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

const modules = [
  CommonModule,
  MatButtonModule,
  HttpClientModule,
  ShopAppModule,
  MatProgressSpinnerModule
]

const components = [
  MenuComponent,
  BodyComponent
]

@NgModule({
  declarations: [
    ...components,
  ],
  imports: [
    ...modules
  ],
  exports: [
    ...components
  ]
})
export class ShopAppLayoutModule { }
