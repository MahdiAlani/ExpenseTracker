import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class YodleeService {
  private apiUrl = 'https://your-backend-api.com/yodlee';  // Replace with your backend API URL

  constructor(private client: HttpClient) {}

}