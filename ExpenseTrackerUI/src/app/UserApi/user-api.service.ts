import { HttpClient } from '@angular/common/http';
import { Injectable, Optional } from '@angular/core';
import { url, User, UserAuth } from './user';

@Injectable({
  providedIn: 'root'
})
export class UserApiService {
  private apiUrl = url + "/api/users"

  constructor(private http: HttpClient) {}
  
  getUser(email: string, password: string) {
    const user: UserAuth = {email: email, password: password};
    console.log(this.http.post<User>(this.apiUrl + "/login", user));
  }

  registerUser(email: string, password: string) {
    const user: UserAuth = {email: email, password: password};
    console.log(this.http.post<User>(this.apiUrl + "/register", user));
  }

}
