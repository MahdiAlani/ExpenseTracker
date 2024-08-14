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

  form: FormGroup;

  constructor(private fb: FormBuilder) {
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
    } 
    else {
      console.log('Form is invalid');
    }
  }
}
