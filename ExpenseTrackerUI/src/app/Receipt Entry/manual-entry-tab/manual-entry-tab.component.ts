import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms'; 
import { CommonModule } from '@angular/common';
import { ReceiptService } from '../../../Services/ReceiptService/receipt.service';
import { Receipt } from '../../../Services/ReceiptService/Receipt';
import { Router } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-manual-entry-tab',
  templateUrl: './manual-entry-tab.component.html',
  styleUrls: ['./manual-entry-tab.component.css'],
  imports: [ReactiveFormsModule, CommonModule]
})
export class ManualEntryTabComponent {

  form: FormGroup;

  constructor(private fb: FormBuilder, private receiptService: ReceiptService, private router: Router) {
    this.form = this.fb.group({
      merchant: ['', Validators.required],
      date: ['', Validators.required],
      category: ['', Validators.required],
      paymentMethod: ['', Validators.required],
      subtotal: [null, Validators.required],
      tax: [null, Validators.required],
      total: [null, Validators.required],
    });
  }

  onSubmit() {
    if (this.form.valid) {
      console.log('Form Submitted', this.form.value);
      var receipt = this.form.value;

      // Set the User Id for the Receipt
      receipt.userId = localStorage.getItem("Id");
      
      this.receiptService.createReceipt(receipt).subscribe({
        next: (response) => {
          console.log('Receipt created successfully:', response);
        },
        error: (error) => {
          console.error('Error creating receipt:', error);
        }
      });
    }
     else {
      console.log('Form is invalid');
    }
    // Refresh window so that components update
    window.location.reload();
  }
}
