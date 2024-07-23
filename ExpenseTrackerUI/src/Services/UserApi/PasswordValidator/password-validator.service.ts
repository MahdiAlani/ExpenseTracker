import { Injectable } from '@angular/core';
import { AbstractControl, ValidationErrors } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class PasswordValidatorService {
  static passwordComplexity(control: AbstractControl): ValidationErrors | null {
    const password = control.value;
    if (!password) {
      return null;
    }

    const errors: ValidationErrors = {};
    if (!/[A-Z]/.test(password)) {
      errors['uppercase'] = true
    }

    if (!/[0-9]/.test(password)) {
      errors['number'] = true;
    }

    return Object.keys(errors).length > 0 ? errors : null;
  }
}
