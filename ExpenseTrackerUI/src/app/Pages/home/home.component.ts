import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ReceiptEntryDialog } from "../../Receipt Entry/receipt-entry-dialog/receipt-entry-dialog";
import { AddReceiptComponent } from "../../Receipt Entry/add-receipt/add-receipt.component";
import { NavBarComponent } from "../../nav-bar/nav-bar.component";
import { ReceiptListComponent } from "../../receipt-list/receipt-list.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterModule, ReceiptEntryDialog, AddReceiptComponent, NavBarComponent, ReceiptListComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

}
