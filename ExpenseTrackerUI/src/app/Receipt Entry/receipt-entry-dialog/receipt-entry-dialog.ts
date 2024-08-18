import { Component } from '@angular/core';
import { UploadTabComponent } from '../upload-tab/upload-tab.component';
import { ManualEntryTabComponent } from '../manual-entry-tab/manual-entry-tab.component';
import { CommonModule } from '@angular/common';
import { bootstrapApplication } from '@angular/platform-browser';

@Component({
  standalone: true,
  selector: 'app-receipt-entry-dialog',
  templateUrl: './receipt-entry-dialog.html',
  styleUrls: ['./receipt-entry-dialog.css'],
  imports: [
    CommonModule,
    UploadTabComponent,
    ManualEntryTabComponent
  ]
})
export class ReceiptEntryDialog {
  activeTab: string = 'upload';

  setActiveTab(tab: string) {
    this.activeTab = tab;
  }
}