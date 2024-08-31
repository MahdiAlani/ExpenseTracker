import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ReceiptEntryDialog } from "../../Receipt Entry/receipt-entry-dialog/receipt-entry-dialog";
import { AddReceiptComponent } from "../../Receipt Entry/add-receipt/add-receipt.component";
import { NavBarComponent } from "../../nav-bar/nav-bar.component";
import { ReceiptListComponent } from "../../DashBoard/receipt-list/receipt-list.component";
import { SpendingsComponent } from "../../DashBoard/spendings/spendings.component";
import { PieChartComponent } from '../../DashBoard/pie-chart/pie-chart.component';
import { MonthlySpendingChartComponent } from '../../DashBoard/monthly-spending-chart/monthly-spending-chart.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterModule, ReceiptEntryDialog, AddReceiptComponent, NavBarComponent, ReceiptListComponent, SpendingsComponent, PieChartComponent, MonthlySpendingChartComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

}
