import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'] // ✅ Fix the typo (styleUrls instead of styleUrl)
})
export class LoginComponent {
  user1 = {
    email: '',
    password: '',
    memberId: ''
  };

  // rememberMe: boolean = false; // Store checkbox state
  loginAttempts: number = 0;
  isLocked: boolean = false;
  timer: number = 30; // Countdown timer (30 seconds)
  timerInterval: any;

  constructor(private authService: AuthService, private router: Router) {}

  Login() {
    // if (this.isLocked) {
    //   Swal.fire({
    //     title: 'Too many failed attempts!',
    //     text: `Please wait ${this.timer} seconds before trying again.`,
    //     icon: 'warning',
    //     confirmButtonText: 'OK'
    //   });
    //   return;
    // }

    if (!this.user1.email || !this.user1.password) {
      Swal.fire({
        title: 'Oops!',
        text: "All fields are required",
        icon: 'error',
        confirmButtonText: 'OK'
      });
      return;
    }

    // Call Login API
    this.authService.Login(this.user1).subscribe({
      next: (response) => {
        if (response.success) {
          Swal.fire({
            title: 'Success!',
            text: response.message,
            icon: 'success',
            confirmButtonText: 'OK'
          });
          sessionStorage.setItem("userEmail", this.user1.email);
          sessionStorage.setItem("UserId", response.data.memberId); // Fix here
          this.router.navigate(['dashboarduser']);
          this.loginAttempts = 0; // Reset attempts on success
        } else {
          this.handleLoginFailure(response.message);
        }
      },
      error: (err) => {
        console.error('Error during login:', err);
        this.handleLoginFailure(err.error?.message || 'Invalid credentials');
      }
    });
  }

  handleLoginFailure(message: string) {
    this.loginAttempts++;

    if (this.loginAttempts > 3) {
      this.isLocked = true;
      this.startCountdown(); // Start 30s countdown
      Swal.fire({
        title: 'Too many failed attempts!',
        text: `Please wait ${this.timer} seconds before trying again.`,
        icon: 'warning',
        confirmButtonText: 'OK'
      });  
    } else {
      Swal.fire({
        title: 'Oops!',
        text: message,
        icon: 'error',
        confirmButtonText: 'OK'
      });
    }
  }

  startCountdown() {
    this.timer = 30; // Reset timer to 30 seconds
    this.timerInterval = setInterval(() => {
      this.timer--;
      if (this.timer <= 0) {
        clearInterval(this.timerInterval);
        this.isLocked = false;
        this.loginAttempts = 0;
      }
    }, 1000); // Decrease every second
  }
}
