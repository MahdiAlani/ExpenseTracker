import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { url, User, UserAuth } from '../user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = `${url}/api/Users`;

  constructor(private http: HttpClient) {}

  loginUser(email: string, password: string) {
    const user: UserAuth = { email, password };
    return this.http.post<User>(`${this.apiUrl}/login`, user, {withCredentials: true})
  }

  registerUser(email: string, password: string) {
    const user: UserAuth = { email, password };
    return this.http.post<User>(`${this.apiUrl}/register`, user, {withCredentials: true});
  }

  private refreshToken() {
    return this.http.post(`${this.apiUrl}/refresh`, {}, {withCredentials: true});
  }

  isAuthenticated(): boolean {
    try {
      this.http.get(`${this.apiUrl}/auth`, {withCredentials: true});
      return true;

    } catch (e: any) {
      return false;
    }
  }
}
