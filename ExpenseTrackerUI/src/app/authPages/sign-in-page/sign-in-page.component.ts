import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../../Services/AuthService/Auth.service';
import { CommonModule } from '@angular/common';
import { User } from '../../../Services/user';

@Component({
  selector: 'app-sign-in-page',
  standalone: true,
  imports: [RouterModule, ReactiveFormsModule, CommonModule],
  templateUrl: './sign-in-page.component.html',
  styleUrl: './sign-in-page.component.css'
})
export class SignInPageComponent {

  loginForm: FormGroup;

  constructor(private api: AuthService, private router: Router) {
    this.loginForm = new FormGroup({
      email: new FormControl(""),
      password: new FormControl("")
    })
  }

  signIn() {
    if (this.loginForm.valid) {
      const { email, password } = this.loginForm.value;
      this.api.loginUser(email, password).subscribe({
        next: (user: User) => {
          console.log('Logged in user:', user);
          this.router.navigate(['']);
        },
        error: (error: any) => {
          console.error('Login error:', error);
        },
        complete: () => {
          console.log('Login request complete');
        }
      });
    }
  }
}
