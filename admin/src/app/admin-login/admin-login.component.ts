import { Component } from '@angular/core';
import { FormGroup, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';  
import { Router } from '@angular/router';  
import { NgIf } from '@angular/common';
import { CommonModule } from '@angular/common';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-admin-login',
  standalone: true,
  imports: [CommonModule, NgIf, ReactiveFormsModule],
  templateUrl: './admin-login.component.html',
  styleUrls: ['./admin-login.component.css']
})
export class AdminLoginComponent {
  loginForm = new FormGroup({
    email: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required)
  });

  errorMessage: string = ''; 
  isLoading: boolean = false;  // Loading state

  constructor(private http: HttpClient, private router: Router,private authService: AuthService) {} 

  onSubmit() {
    if (this.loginForm.invalid) {
      this.errorMessage = 'Please enter both email and password';
      return;
    }
  
    this.isLoading = true;
    const adminData = { 
      Email: this.loginForm.get('email')?.value, 
      Password: this.loginForm.get('password')?.value 
    };
  
    this.http.post('https://localhost:7256/api/AdminLogin/login', adminData)
      .subscribe({
        next: (response: any) => {
          console.log("Login successful, received token:", response.token);
          localStorage.setItem('adminToken', response.token);  // ✅ Store token
  
          alert("Login Successful");
          this.isLoading = false; 
          this.router.navigate(['/admin-dashboard']);
        },
        error: () => {
          this.isLoading = false;
          this.errorMessage = 'Invalid Email or Password';
        }
      });
  }
}
