import { Component, OnInit } from '@angular/core';
import { ChartModule } from 'primeng/chart';
import { ReceiptService } from '../../../Services/ReceiptService/receipt.service'; // Your service to fetch data
import { CommonModule } from '@angular/common';
import { PeriodicData } from '../../../Services/ReceiptService/Receipt';

@Component({
  selector: 'app-pie-chart',
  standalone: true,
  imports: [ChartModule, CommonModule],
  templateUrl: './pie-chart.component.html',
  styleUrls: ['./pie-chart.component.css']
})
export class PieChartComponent implements OnInit {

  chartData: any;
  chartOptions: any;

  constructor(private receiptService: ReceiptService) { }

  ngOnInit(): void {
    this.initializeChart();
  }

  initializeChart() {
    // Get the current date and calculate the start and end of the current month
    const currentDate = new Date();
    const startDate = new Date(currentDate.getFullYear(), currentDate.getMonth(), 1); // Start of current month
    const endDate = new Date(currentDate.getFullYear(), currentDate.getMonth() + 1, 0); // End of current month

    // Fetch data from the service for the current month and category
    this.receiptService.getSpendingsData(startDate, endDate, '', 'category').subscribe((data: PeriodicData[]) => {
      console.log(data)
      if (data && data.length > 0) {  // Check if data is not empty
        // Prepare chart data using the TypeData format
        this.chartData = {
          labels: data.map(d => d.type), // Extract categories for labels
          datasets: [
            {
              data: data.map(d => d.totalSpent), // Extract totalSpent for each category
              backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56', '#4BC0C0', '#9966FF'], // Different colors for each category
              hoverBackgroundColor: ['#FF6384', '#36A2EB', '#FFCE56', '#4BC0C0', '#9966FF']
            }
          ]
        };
      } else {
        console.warn('No data available for the chart.');
      }
    });

    // Define chart options
    this.chartOptions = {
      responsive: true, // Makes the chart responsive to different screen sizes
      plugins: {
        legend: {
          display: true, // Display the legend
          position: 'left', // Position of the legend ('top', 'bottom', 'left', 'right')
          labels: {
            color: '#333', // Color of the legend text
            font: {
              size: 14, // Font size for legend text
            }
          }
        },
        tooltip: {
          enabled: true, // Enable tooltips
          callbacks: {
            label: function(tooltipItem: any) {
              return `${tooltipItem.label}: $${tooltipItem.raw.toFixed(2)}`; // Customize the tooltip label to show the value in currency format
            }
          }
        }
      },
      scales: {
        x: { display: false }, // Disable the grid lines for the X-axis
        y: { display: false }  // Disable the grid lines for the Y-axis
      },
      elements: {
        arc: {
          borderWidth: 2 // Border width of the pie segments
        }
      }
    };
  }
}
