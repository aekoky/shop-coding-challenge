import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UtilitiesService {
  private readonly _notify: Subject<string>

  constructor(
    private readonly _snackBar: MatSnackBar) {
    this._notify = new Subject<string>();
    this._notify.subscribe(message => {
      this._snackBar.open(message);
    });
  }
  
  public notify(message: string): void {
    if (this._notify) {
      this._notify.next(message);
    }
  }
}
