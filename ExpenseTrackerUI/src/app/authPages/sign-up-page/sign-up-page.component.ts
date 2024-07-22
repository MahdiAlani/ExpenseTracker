import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../../Services/UserApi/Auth.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-sign-up-page',
  standalone: true,
  imports: [RouterModule, FormsModule],
  templateUrl: './sign-up-page.component.html',
  styleUrls: ['./sign-up-page.component.css']
})
export class SignUpPageComponent {

  email: string = '';
  password: string = '';

  constructor(private api: AuthService) {}

  register() {
    this.api.registerUser(this.email, this.password).subscribe(
      response => {
        console.log('Response:', response);
      }
    );
  }
}

