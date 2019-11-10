import { Validators, FormGroup } from '@angular/forms';

export class PasswordValidator {

    constructor(){

    }
    passwordStrong() {
        return Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{8,}');
    }

    checkPasswordsAreTheSame(formGroup: FormGroup) {
        let password = formGroup.get('password');
        let confirmPassword = formGroup.get('confirmPassword');
        if (!password || !confirmPassword)
          return null;
        return password.value === confirmPassword.value === true ? null : { notSame: true }
      }
}
