import { Component, OnInit, OnDestroy } from '@angular/core';
import { UserModel } from 'src/app/models/user.model';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { AuthenticationApiService } from 'src/app/services/authentication-api.service';
import { LayoutService } from 'src/app/services/layout.service';
import { Views } from 'src/app/enums/views.enum';
import { UtilitiesService } from 'src/app/services/utilities.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-user-signin',
  templateUrl: './user-signin.component.html',
  styleUrls: ['./user-signin.component.scss']
})
export class UserSigninComponent implements OnInit, OnDestroy {
  private _userForm: FormGroup;
  private readonly _subscriptions: Subscription;

  public get userForm(): FormGroup {
    return this._userForm;
  }

  constructor(
    private readonly _formBuilder: FormBuilder,
    private readonly _authenticationApiService: AuthenticationApiService,
    private readonly _layoutService: LayoutService,
    private readonly _utilitiesService: UtilitiesService
  ) {
  }

  ngOnInit() {
    this._userForm = this._formBuilder.group({
      email: new FormControl('', [Validators.email, Validators.required]),
      password: new FormControl('', [Validators.required])
    });
  }

  ngOnDestroy(): void {
    if (this._subscriptions)
      this._subscriptions.unsubscribe();
  }

  onSubmit() {
    const userModel = <UserModel>{ email: this._userForm.controls.email.value, password: this._userForm.controls.password.value };
    const userSubscription = this._authenticationApiService.authenticateUser(userModel).subscribe(
      user => {
        if (user) {
          this._utilitiesService.notify("loged in"); //TODO : Work on the message
          this._layoutService.setViewStat(Views.Shops);
        }
      },
      err => {
        this._utilitiesService.notify("Error while loging in"); //TODO : Work on the message
      });
    this._subscriptions.add(userSubscription);
  }
}
