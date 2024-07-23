import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../../Services/UserApi/Auth.service';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PasswordValidatorService } from '../../../Services/UserApi/PasswordValidator/password-validator.service';

@Component({
  selector: 'app-sign-up-page',
  standalone: true,
  imports: [RouterModule, FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './sign-up-page.component.html',
  styleUrls: ['./sign-up-page.component.css']
})
export class SignUpPageComponent {

  registrationForm: FormGroup;

  constructor(private api: AuthService, private passValidator: PasswordValidatorService) {
    this.registrationForm = new FormGroup({
      email: new FormControl("", [Validators.required, Validators.email]),
      password: new FormControl("", [Validators.required, Validators.minLength(8), PasswordValidatorService.passwordComplexity])
    })
  }

  register() {
    if (this.registrationForm.valid) {
      const {email, password} = this.registrationForm.value
      this.api.registerUser(email, password).subscribe(
        response => {
          console.log('Response:', response);
        }
      );
    }
    
  }
}