import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Receipt } from './Receipt';
import { url } from '../UserApi/user';

@Injectable({
  providedIn: 'root'
})
export class ReceiptService {

  private apiUrl = `${url}/api/receipts`

  constructor(private client: HttpClient) { }

  createReceipt(receipt: Receipt) {
    return this.client.post(this.apiUrl, receipt);
  }
}