import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { url, User, UserAuth } from '../user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = `${url}/api/Users`;

  constructor(private client: HttpClient) {}

  loginUser(email: string, password: string) {
    const user: UserAuth = { email, password };
    return this.client.post<User>(`${this.apiUrl}/login`, user, {withCredentials: true})
  }

  registerUser(email: string, password: string) {
    const user: UserAuth = { email, password };
    return this.client.post<User>(`${this.apiUrl}/register`, user, {withCredentials: true});
  }

  logOutUser() {
    localStorage.clear();
    return this.client.post(`${this.apiUrl}/logout`, {}, {withCredentials: true});
  }

  private refreshToken() {
    return this.client.post(`${this.apiUrl}/refresh`, {}, {withCredentials: true});
  }

  isAuthenticated(): Observable<boolean> {
    const userEmail = localStorage.getItem('Email');
    const userId = localStorage.getItem('Id');
  
    if (!userEmail || !userId) {
      // If either is missing, return false
      return of(false);
    }

    return this.client.get(`${this.apiUrl}/auth`, { observe: 'response', withCredentials: true }).pipe(
      map((response) => {
        // Check if response was valid
        return response.status === 200;
      }),
      catchError(error => {
        return [false];
      })
    );
  }
}
