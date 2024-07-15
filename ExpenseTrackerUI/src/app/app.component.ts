import { Component } from '@angular/core';
import { AddReceiptComponent } from './Receipt Entry/add-receipt/add-receipt.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { SignUpPageComponent } from "./authPages/sign-up-page/sign-up-page.component";
import { SignInPageComponent } from "./authPages/sign-in-page/sign-in-page.component";
import { RouterModule } from '@angular/router';
import { HomeComponent } from './Pages/home/home.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [HomeComponent, RouterModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'ExpenseTrackerUI';
}