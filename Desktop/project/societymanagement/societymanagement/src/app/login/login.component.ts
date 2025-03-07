import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { response } from 'express';
import Swal from 'sweetalert2';


@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  user1={
    email : '',
    password : '',
    memberId : ''
  };

 

    constructor(private authService: AuthService, private router: Router) {}

    Login()
    {
        if(!this.user1.email || !this.user1.password )
        {
          // alert("All fields are required");
          Swal.fire({
            title: 'Oops!',
            text: "All fields are required",
            icon: 'error',
            confirmButtonText: 'OK'
          });
          return;
        }



      //2. Call Login API
    this.authService.Login(this.user1).subscribe({
      next: (response) => {
        if (response.success) {
          // alert(response.message);
             Swal.fire({
                  title: 'Success!',
                  text: response.message,
                  icon: 'success',
                  confirmButtonText: 'OK'
                });
          sessionStorage.setItem("userEmail",this.user1.email);
          sessionStorage.setItem("UserId", response.data.memberId); // <-- Fix here

          this.router.navigate(['dashboarduser']);
          // ✅ Navigate only on successful login
        } 
        else{
          Swal.fire({
            title: 'Oops!',
            text: response.message,
            icon: 'error',
            confirmButtonText: 'OK'
          });
        }
      },
      error: (err) => {
        console.error('Error during login:', err);
        if (err.error && err.error.message) {
          // alert(err.error.message); // ✅ Show backend error message
          Swal.fire({
            title: 'Oops!',
            text: err.error.message,
            icon: 'error',
            confirmButtonText: 'OK'
          });
        } 
      }
    });
    }

    
  
}

