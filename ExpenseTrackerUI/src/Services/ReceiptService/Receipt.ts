import { Type } from "@angular/core";

export interface Receipt {
    userId: number;
    merchant: string;
    date: Date;
    category: 'food' | 'travel' | 'utilities' | 'entertainment' | 'other';
    paymentMethod: 'cash' | 'creditCard' | 'debitCard' | 'other';
    subtotal: number;
    tax: number;
    total: number;
}

export const exampleReceipts: Receipt[] = [
    {
        userId: 1,
        merchant: 'Grocery Mart',
        date: new Date('2024-08-14'),
        category: 'food',
        paymentMethod: 'creditCard',
        subtotal: 45.99,
        tax: 3.68,
        total: 49.67
    },
    {
        userId: 2,
        merchant: 'Travel Express',
        date: new Date('2024-08-10'),
        category: 'travel',
        paymentMethod: 'debitCard',
        subtotal: 120.00,
        tax: 9.60,
        total: 129.60
    },
    {
        userId: 3,
        merchant: 'Electricity Co.',
        date: new Date('2024-08-01'),
        category: 'utilities',
        paymentMethod: 'cash',
        subtotal: 75.00,
        tax: 0.00,
        total: 75.00
    },
    {
        userId: 4,
        merchant: 'Movie Theater',
        date: new Date('2024-08-12'),
        category: 'entertainment',
        paymentMethod: 'creditCard',
        subtotal: 22.00,
        tax: 1.76,
        total: 23.76
    },
    {
        userId: 5,
        merchant: 'Bookstore',
        date: new Date('2024-08-07'),
        category: 'other',
        paymentMethod: 'debitCard',
        subtotal: 18.50,
        tax: 1.48,
        total: 19.98
    }
];

export const exampleData = {
    labels: [
      'January', 'February', 'March', 'April', 'May', 'June', 
      'July', 'August', 'September', 'October', 'November', 'December'
    ],
    datasets: [
      {
        label: 'Total Spendings ($)',
        data: [450, 380, 560, 700, 490, 680, 520, 610, 450, 730, 640, 590], // Example spending for each month
        backgroundColor: '#36A2EB', // Single color for all bars
        borderColor: '#36A2EB', // Same color for the border
        borderWidth: 1 // Border width for the bars
      }
    ]
  };

export interface PeriodicData {
    month: string;
    year: string
    type: string
    totalSpent: number
}

export interface TypeData {
    type: string
    totalSpent: number
}
