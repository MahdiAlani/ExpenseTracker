import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../../Services/AuthService/Auth.service';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PasswordValidatorService } from '../../../Services/PasswordValidator/password-validator.service';
import { User } from '../../../Services/user';

@Component({
  selector: 'app-sign-up-page',
  standalone: true,
  imports: [RouterModule, FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './sign-up-page.component.html',
  styleUrls: ['./sign-up-page.component.css']
})
export class SignUpPageComponent {

  registrationForm: FormGroup;

  constructor(private api: AuthService, private passValidator: PasswordValidatorService, private router: Router) {
    this.registrationForm = new FormGroup({
      email: new FormControl("", [Validators.required, Validators.email]),
      password: new FormControl("", [Validators.required, Validators.minLength(8), PasswordValidatorService.passwordComplexity])
    })
  }

  register() {
    if (this.registrationForm.valid) {
      const { email, password } = this.registrationForm.value;
      this.api.registerUser(email, password).subscribe({
        next: (user: User) => {
          console.log('Registered in user:', user);
          this.router.navigate(['']);
        },
        error: (error: any) => {
          console.error('Registration error:', error);
        },
        complete: () => {
          console.log('Registration request complete');
        }
      });
    }
  }
}