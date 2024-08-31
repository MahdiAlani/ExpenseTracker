import { Component, OnInit } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { Observable } from 'rxjs';
import { ReceiptService } from '../../../Services/ReceiptService/receipt.service';
import { CommonModule } from '@angular/common';
import { TypeData } from '../../../Services/ReceiptService/Receipt';

@Component({
  selector: 'app-spendings',
  standalone: true,
  imports: [MatCardModule, CommonModule],
  templateUrl: './spendings.component.html',
  styleUrl: './spendings.component.css'
})
export class SpendingsComponent implements OnInit {

  protected total$: Observable<TypeData> = new Observable<TypeData>();
  
  // Needed to make first call that subscribes to the observable
  ngOnInit(): void {
    const startDate = new Date(new Date().getFullYear(), new Date().getMonth(), 1); // First day of the current month
    this.total$ = this.receiptService.sumReceiptTotals('month', ''); // Assign the observable to receipts$
  }

  constructor(private receiptService: ReceiptService) { }
}
