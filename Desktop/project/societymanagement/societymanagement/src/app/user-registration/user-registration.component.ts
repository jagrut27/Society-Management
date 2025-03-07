import { Component } from '@angular/core';
import { AuthService } from '../auth.service'; // Import AuthService for API calls
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-user-registration',
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.css'],
  standalone: false
})
export class UserRegistrationComponent {
  user = {
    firstname: '',
    lastname: '',
    email: '',
    password: '',
    confirmPassword: '',
    phoneNumber: '',
    
    flatNumber: '',
    blockNumber: ''
  };

  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  validateForm(): boolean {
    this.errorMessage = '';

    if (!this.user.firstname || !this.user.lastname || !this.user.email ||
        !this.user.password || !this.user.confirmPassword ||
        !this.user.flatNumber || !this.user.phoneNumber || !this.user.blockNumber) {
      // alert("All Field are required..")

      Swal.fire({
        title: 'Oops!',
        text: 'All fields are required.',
        icon: 'warning',
        confirmButtonText: 'OK'
      });
      
      return false;
    }

    // const passwordPattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;

    // if (!passwordPattern.test(this.user.password)) {
    //   this.errorMessage = "Password must be at least 6 characters long and include an uppercase letter, lowercase letter, number, and special character.";
    //   return false;
    // }

    if (this.user.password.length < 6) {
      this.errorMessage = "Password must be at least 6 characters.";
      return false;
    }

    if (this.user.password !== this.user.confirmPassword) {
      this.errorMessage = "Passwords do not match.";
      return false;
    }

    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailPattern.test(this.user.email)) {
      this.errorMessage = "Invalid email format.";
      return false;
    }

    const phonePattern = /^\d{10}$/;
    if (!phonePattern.test(this.user.phoneNumber)) {
      this.errorMessage = "Phone number must be 10 digits.";
      return false;
    }

    return true;
  }

  register() {
    if (!this.validateForm()) {
      return; // Stop if validation fails
    }

    this.authService.register(this.user).subscribe({
      next: (response) => {
        if (response.success) {
          console.log('Registration successful:', response);
          alert(response.message);
          window.location.reload();
        }
        else{
          console.log("Else block");
            Swal.fire({
                  title: 'Oops!',
                  text: response.message,
                  icon: 'error',
                  confirmButtonText: 'OK'
                });
        }
      },
      error: (err) => {
        console.error('Error during registration:', err);
        // this.errorMessage = "Error during registration. Please try again.";
        if (err.error && err.error.message) {
        Swal.fire({
          title: 'Oops!',
          text:  err.error.message, // Display actual error
          icon: 'error',
          confirmButtonText: 'OK'
        });
        }
      }
    });
  }
}
