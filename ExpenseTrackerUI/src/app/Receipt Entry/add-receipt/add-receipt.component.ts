import { Component } from '@angular/core';
import { ReceiptEntryDialog } from '../receipt-entry-dialog/receipt-entry-dialog';

@Component({
  standalone: true,
  selector: 'add-receipt',
  templateUrl: './add-receipt.component.html',
  styleUrls: ['./add-receipt.component.css'],
  imports: [ReceiptEntryDialog]
})
export class AddReceiptComponent {}
