import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Receipt } from './Receipt';
import { url } from '../user';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class ReceiptService {

  private apiUrl = `${url}/api/receipts`

  constructor(private client: HttpClient) { }

  createReceipt(receipt: Receipt) {
    return this.client.post(this.apiUrl, receipt);
  }

  getReceipts(column: string, order: string): Observable<Receipt[]> {
    const id = localStorage.getItem("Id");
    console.log(id)
    const params = {
      sortBy: column,
      sortDirection: order
    };
  
    return this.client.get<Receipt[]>(`${this.apiUrl}/${id}`, { params });
  }
}