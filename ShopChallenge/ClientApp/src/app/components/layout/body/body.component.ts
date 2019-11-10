import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subject, Subscription } from 'rxjs';
import { Views } from 'src/app/enums/views.enum';
import { LayoutService } from 'src/app/services/layout.service';
import { distinctUntilChanged } from 'rxjs/operators';

@Component({
  selector: 'app-body',
  templateUrl: './body.component.html',
  styleUrls: ['./body.component.scss']
})
export class BodyComponent implements OnInit, OnDestroy {
  views = Views;
  _view: Observable<Views>;

  public get viewState(): Observable<Views> {
    return this._view;
  }

  constructor(
    private readonly _layoutService: LayoutService
  ) {
    this._view = this._layoutService.viewState.pipe(distinctUntilChanged());
  }

  ngOnInit() {
  }

  ngOnDestroy(): void {
  }
}
