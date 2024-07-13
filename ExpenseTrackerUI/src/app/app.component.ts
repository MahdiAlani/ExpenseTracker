import { Component } from '@angular/core';
import { AddReceiptComponent } from './Receipt Entry/add-receipt/add-receipt.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { SignUpPageComponent } from "./sign-up-page/sign-up-page.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [AddReceiptComponent, NavBarComponent, SignUpPageComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'ExpenseTrackerUI';
}