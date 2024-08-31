import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
interface Category {
  name: string;
  budget: number;
  spent: number;
}

@Component({
  selector: 'app-budget-progress',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './budget-progress.component.html',
  styleUrls: ['./budget-progress.component.css']
})
export class BudgetProgressComponent implements OnInit {
  categories: Category[] = [
    { name: 'Food', budget: 200, spent: 120 },
    { name: 'Travel', budget: 150, spent: 60 },
    { name: 'Utilities', budget: 100, spent: 80 },
    { name: 'Entertainment', budget: 120, spent: 90 },
    { name: 'Other', budget: 50, spent: 20 }
  ];

  constructor() {}

  ngOnInit(): void {}

  updateProgress(category: Category): void {
    // This function can be used to handle changes when the budget is updated.
    // For now, it just recalculates the progress. Additional logic can be added here if needed.
  }

  getProgressBarType(category: Category): string {
    const percentage = (category.spent / category.budget) * 100;
    if (percentage < 75) {
      return 'success'; // Green bar for under 75% of the budget
    } else if (percentage < 100) {
      return 'warning'; // Yellow bar for 75%-99% of the budget
    } else {
      return 'danger'; // Red bar for over budget
    }
  }
}
