import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms'; 
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-manual-entry-tab',
  templateUrl: './manual-entry-tab.component.html',
  styleUrls: ['./manual-entry-tab.component.css'],
  imports: [ReactiveFormsModule, CommonModule]
})
export class ManualEntryTabComponent {

  expenseForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.expenseForm = this.fb.group({
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
    debugger
    if (this.expenseForm.valid) {
      console.log('Form Submitted', this.expenseForm.value);
    } 
    else {
      console.log('Form is invalid');
    }
  }
}
