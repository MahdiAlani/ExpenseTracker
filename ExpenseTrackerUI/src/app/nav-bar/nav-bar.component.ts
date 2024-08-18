import { Component } from '@angular/core';
import { AuthService } from '../../Services/AuthService/Auth.service';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css'
})
export class NavBarComponent {
  protected email = localStorage.getItem('Email')

  constructor(private authService: AuthService, private router: Router) { }

  logout() {
    this.authService.logOutUser().subscribe();
    this.router.navigate(['SignIn'])
  }
}
