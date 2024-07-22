import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { url, User, UserAuth } from './user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${url}/api/Users`;

  constructor(private http: HttpClient) {}

  loginUser(email: string, password: string): Observable<User> {
    const user: UserAuth = { email, password };
    return this.http.post<User>(`${this.apiUrl}/login`, user)
  }

  registerUser(email: string, password: string): Observable<User> {
    const user: UserAuth = { email, password };
    return this.http.post<User>(`${this.apiUrl}/register`, user);
  }
}
