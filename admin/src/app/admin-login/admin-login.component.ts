import { Component } from '@angular/core';
import { FormGroup, FormControl,ReactiveFormsModule  } from '@angular/forms';
import { HttpClient } from '@angular/common/http';  
import { NgIf } from '@angular/common';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin-login',
  standalone:true,
  imports:[CommonModule,NgIf,ReactiveFormsModule],
  templateUrl: './admin-login.component.html',
  styleUrls: ['./admin-login.component.css']
})
export class AdminLoginComponent {
  loginForm = new FormGroup({
    email: new FormControl(''),
    password: new FormControl('')
  });

  errorMessage: string = ''; 

  constructor(private http: HttpClient) {} 

  onSubmit() {
    const adminData = { 
      Email: this.loginForm.get('email')?.value, 
      Password: this.loginForm.get('password')?.value 
    };

    this.http.post('http://localhost:5153/api/AdminLogin/login', adminData)
      .subscribe({
        next: (response: any) => {
          console.log('Login successful:', response);
        },
        error: (error) => {
          this.errorMessage = 'Invalid Email or Password';
          console.error('Error:', error);
        }
      });
  }
}
