export interface Receipt {
    merchant: string;
    date: Date;
    category: 'food' | 'travel' | 'utilities' | 'entertainment' | 'other';
    paymentMethod: 'cash' | 'creditCard' | 'debitCard' | 'other';
    subtotal: number;
    tax: number;
    total: number;
}