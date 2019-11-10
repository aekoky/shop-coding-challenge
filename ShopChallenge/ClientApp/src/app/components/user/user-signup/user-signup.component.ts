import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { Views } from 'src/app/enums/views.enum';
import { LayoutService } from 'src/app/services/layout.service';
import { UserApiService } from 'src/app/services/user-api.service';
import { UtilitiesService } from 'src/app/services/utilities.service';
import { PasswordValidator } from 'src/app/validators/password-validator';

@Component({
  selector: 'app-user-signup',
  templateUrl: './user-signup.component.html',
  styleUrls: ['./user-signup.component.scss']
})
export class UserSignupComponent implements OnInit, OnDestroy {
  private readonly _passwordValidator: PasswordValidator;
  private readonly _userFormObservable: Observable<FormGroup>;
  private readonly _userForm: FormGroup;

  public get userForm(): Observable<FormGroup> {
    return this._userFormObservable;
  }

  constructor(
    private readonly _formBuilder: FormBuilder,
    private readonly _userApiService: UserApiService,
    private readonly _layoutService: LayoutService,
    private readonly _utilitiesService: UtilitiesService
  ) {
    this._passwordValidator = new PasswordValidator();
    this._userForm = this._formBuilder.group({
      email: new FormControl('', [Validators.email, Validators.required]),
      password: new FormControl('', [Validators.required, this._passwordValidator.passwordStrong]),
      confirmPassword: new FormControl('', [Validators.required, this._passwordValidator.checkPasswordsAreTheSame]),
      coordinates: this._formBuilder.group({
        longitude: new FormControl('', [Validators.required]),
        latitude: new FormControl('', [Validators.required])
      })
    });
    this._userFormObservable = of(this._userForm);
  }

  ngOnInit() {
  }

  ngOnDestroy(): void {
  }

  onSubmit() {
    this._userApiService.signUpUser(this._userForm.value)
      .then(
        () => {
          this._layoutService.setViewStat(Views.Shops);
          this._utilitiesService.notify("user signed up");
        },
        err => {
          this._utilitiesService.notify("user failed to signed up");
          console.log(err);
        }
      );
  }
}
