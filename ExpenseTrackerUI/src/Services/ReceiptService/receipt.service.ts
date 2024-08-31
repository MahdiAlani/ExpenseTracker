import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, Type } from '@angular/core';
import { PeriodicData, Receipt, TypeData } from './Receipt';
import { url } from '../user';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class ReceiptService {

  private id: string | null = localStorage.getItem('Id');
  private apiUrl = `${url}/api/receipts`

  constructor(private client: HttpClient) { }

  createReceipt(receipt: Receipt) {
    return this.client.post(this.apiUrl, receipt);
  }
  
  deleteReceipt(receipt: Receipt) {

  }

  editReceipt(receipt: Receipt) {

  }

  getReceipts(column: string, order: string): Observable<Receipt[]> {
    const params = {
      sortBy: column,
      sortDirection: order
    };
  
    return this.client.get<Receipt[]>(`${this.apiUrl}/${this.id}`, { params });
  }

  sumReceiptTotals(term: string, filter: string) {
    const params = new HttpParams()
      .set('term', term)
      .set('filter', filter)

    return this.client.get<TypeData>(`${this.apiUrl}/totalSpendings/${this.id}`, { params })
    .pipe(
      map(data => data) // Extract totalSpent or return 0 if no data
    );
  }

  getSpendingsData(startDate: Date, endDate: Date, frequency: string, filter: string): Observable<PeriodicData[]> {
    const freqOptions = ['weekly', 'monthly', 'yearly'];
    const filterOptions = ['category', 'paymentmethod'];
    // Incorrect frequency value
    if (frequency !== "" && !freqOptions.includes(frequency.toLowerCase())) {
      frequency = ''
    }
    else {
      frequency = frequency.toLowerCase()
    }

    // Incorrect filter value
    if (filter !== "" && !filterOptions.includes(filter.toLowerCase())) {
      filter = ''
    }
    else {
      filter = filter.toLowerCase()
    }
    const params = new HttpParams()
      .set('startDate', startDate.toISOString())
      .set('endDate', endDate.toISOString())
      .set('frequency', frequency)
      .set('filter', filter);
      return this.client.get<PeriodicData[]>(`${this.apiUrl}/spendingsList/${this.id}`, { params }).pipe(
        map(response => {
          // Transform server response to the desired type PeriodicData[]
          return response.map(item => ({
            month: item.month,
            year: item.year,
            type: item.type, // Adjust this depending on the server response structure
            totalSpent: item.totalSpent
          }));
        })
      );
    }
  }