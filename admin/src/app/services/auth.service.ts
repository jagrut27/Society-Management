import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor() {}

  isAuthenticated(): boolean {
    const token = localStorage.getItem('adminToken');
    console.log("AuthService: Checking if adminToken exists ->", token ? "YES" : "NO");
    return !!token;  // ✅ Returns true if token exists, false otherwise
  }

  login(token: string): void {
    console.log("AuthService: Storing adminToken ->", token);
    localStorage.setItem('adminToken', token);  // ✅ Store token on login
  }

  logout(): void {
    console.log("AuthService: Removing adminToken");
    localStorage.removeItem('adminToken');  // ✅ Remove token on logout
  }
}