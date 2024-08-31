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

  loginForm: FormGroup; // The form used for user input
  wrongCredentials: Boolean = false; // Used to display error messages
  loading: Boolean = false; // Displays loading bar for user

  constructor(private api: AuthService, private router: Router) {
    this.loginForm = new FormGroup({
      email: new FormControl("", Validators.required),
      password: new FormControl("", Validators.required)
    })

    // When the user starts typing, wrong credentials message is removed
    this.loginForm.valueChanges.subscribe(() => {
      this.wrongCredentials = false;
    });
  }

  signIn() {
    // Start loading
    this.loading = true;

    if (this.loginForm.valid) {
      const { email, password } = this.loginForm.value;
      this.api.loginUser(email, password).subscribe({
        next: (user: User) => {
          console.log('Logged in user:', user);

          // Save user into local storage
          localStorage.setItem("Email", user.email)
          localStorage.setItem("Id", user.id.toString())

          // Navigate to the home page
          this.router.navigate(['']);
        },
        error: (error: any) => {
          console.error('Login error');
          // Stop loading
          this.loading = false;
          this.wrongCredentials = true;
        },
        complete: () => {
          console.log('Login request complete');
        }
      });
    } else {
      // Stop loading
      this.loading = false;
    }
  }
}
