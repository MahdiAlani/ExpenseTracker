import { Component, OnInit } from '@angular/core';
import { ReceiptService } from '../../../Services/ReceiptService/receipt.service';
import { exampleData, PeriodicData } from '../../../Services/ReceiptService/Receipt';
import { ChartModule } from 'primeng/chart';

@Component({
  selector: 'app-monthly-spending-chart',
  standalone: true,
  imports: [ChartModule],
  templateUrl: './monthly-spending-chart.component.html',
  styleUrl: './monthly-spending-chart.component.css'
})
export class MonthlySpendingChartComponent implements OnInit {

  chartData: any;
  chartOptions: any;

  constructor(private receiptService: ReceiptService) { }

  ngOnInit(): void {
    this.initializeChart()
  }

  initializeChart() {
    const currentDate = new Date();
    const startDate = new Date(currentDate.getFullYear(), 0, 1); // January 1st of the current year
    const endDate = new Date(currentDate.getFullYear(), 11, 31); // December 31st of the current year
  
    // Fetch data from the service for the current year with 'monthly' frequency
    this.receiptService.getSpendingsData(startDate, endDate, 'monthly', '').subscribe(data => {
      // Initialize an array with zeros for all months
      const monthlyData = new Array(12).fill(0);
  
      // Fill the monthlyData array with the data returned from the service
      data.forEach(d => {
        const monthIndex = parseInt(d.month) - 1; // Convert 1-based month to 0-based index
        monthlyData[monthIndex] = d.totalSpent;
      });
  
      // Setup the chart data with all months labels and the fetched data
      this.chartData = exampleData
      // this.chartData = {
      //   labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'], // Labels for each month
      //   datasets: [
      //     {
      //       label: 'Monthly Spending',
      //       data: monthlyData, // Data array with total spending for each month
      //       backgroundColor: '#36A2EB', // Color of the bars
      //       borderColor: '#000000', // Border color of the bars
      //       borderWidth: 1, // Border width of the bars
      //     },
      //   ],
      // };
    });
    // Define chart options
    this.chartOptions = {
      responsive: true,
      maintainAspectRatio: true, // Allows the chart to resize to fill its container
      scales: {
        x: {
          display: true,
          title: {
            display: true,
            text: 'Month'
          },
          grid: {
            display: false, // Hide X-axis grid lines for a cleaner look
            drawBorder: true, // Show the border around the grid
          },
          ticks: {
            maxRotation: 0, // Prevent label rotation to save space
            minRotation: 0,
          }
        },
        y: {
          display: true,
          title: {
            display: true,
            text: 'Total Spending ($)'
          },
          beginAtZero: true, // Start Y-axis at zero
          grid: {
            display: true,
            drawBorder: false, // Remove border to make it look cleaner
            drawOnChartArea: true, // Ensure grid lines extend over the entire chart area
          },
          ticks: {
            stepSize: 100, // Increase number of ticks to make the chart more "zoomed in"
            maxTicksLimit: 8, // Maximum number of ticks to display on the Y-axis
            padding: 10, // Slightly increase padding between ticks for spacing
          }
        },
      },
      plugins: {
        legend: {
          display: false, // Show legend like in the example
        },
        tooltip: {
          enabled: true, // Enable tooltips
        }
      },
      layout: {
      }
    };
    

    
    
  }
}
