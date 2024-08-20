import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Receipt } from '../../../Services/ReceiptService/Receipt';
import { ReceiptService } from '../../../Services/ReceiptService/receipt.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-receipt-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './receipt-list.component.html',
  styleUrl: './receipt-list.component.css'
})
export class ReceiptListComponent implements OnInit {

  protected currentSort = { column: 'date', order: 'desc' };
  protected receipts$: Observable<Receipt[]> = new Observable<Receipt[]>();
  
  // Needed to make first call that subscribes to the observable
  ngOnInit(): void {
    this.receipts$ = this.receiptService.getReceipts('date', 'desc'); // Assign Observable to receipts$
  }

  constructor(private receiptService: ReceiptService) { }

  sortBy(column: string) {

    // Different Column
    if (this.currentSort.column !== column) {
      this.currentSort = { column: column, order: 'asc' }
    }
    // Same Column, descending
    else if (this.currentSort.order === 'desc') {
      this.currentSort.order = 'asc'
    }
    // Same Column, ascending
    else {
      this.currentSort.order = 'desc'
    }
    
    // Change order based on current sorting options
    this.receipts$ = this.receiptService.getReceipts(this.currentSort.column, this.currentSort.order);
  }
}
