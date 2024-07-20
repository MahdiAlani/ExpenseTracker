import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UserApiService } from '../../UserApi/user-api.service';

@Component({
  selector: 'app-sign-up-page',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './sign-up-page.component.html',
  styleUrl: './sign-up-page.component.css'
})
export class SignUpPageComponent {

  constructor(private api: UserApiService) {}
  
  register(email: string, password: string) {
    this.api.registerUser(email, password)
  }
}
