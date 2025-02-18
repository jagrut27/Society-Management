import { Component, } from '@angular/core';
import { AuthService } from '../auth.service'; // Import the auth service for API calls
import { Router } from '@angular/router';


@Component({
  selector: 'app-user-registration',
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.css'],
  standalone:false

})
export class UserRegistrationComponent{
  user = {
    username: '',
    email: '',
    password: ''
  };

  errorMessage = '';

  constructor(private authService: AuthService, private router: Router) {}

  register() {
    // Call the register API using the AuthService
    this.authService.register(this.user).subscribe({
      next: (response) => {
        console.log('Registration successful:', response);
        alert('User Registered Successfully');

        window.location.reload(); 
        // this.router.navigate(['/login']); // Redirect to the login page
      },
      error: (err) => {
        console.error('Error during registration:', err);
        alert("Error During the Registration");
      }
    });
  }

  
} 


