import { Component } from '@angular/core';
import { AddReceiptComponent } from './Receipt Entry/add-receipt/add-receipt.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [AddReceiptComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'ExpenseTrackerUI';
}