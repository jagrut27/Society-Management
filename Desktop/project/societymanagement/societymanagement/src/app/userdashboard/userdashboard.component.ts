import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService, userProfile } from '../auth.service'; // Make sure the AuthService is correctly imported

@Component({
  selector: 'app-userdashboard',
  standalone: false,
  templateUrl: './userdashboard.component.html',
  styleUrls: ['./userdashboard.component.css']
})
export class UserdashboardComponent implements OnInit {
  isDropdownOpen = false;
  isClosed = false;
  userEmail: string | null = null;
  constructor(private router: Router, private authService: AuthService, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    let storedEmail = sessionStorage.getItem('userEmail');
    console.log('Logged-in user email:', storedEmail); // Log to ensure email is retrieved

    if (!storedEmail) {
      console.error('No user email found in session storage.');
      this.router.navigate(['/login']); 
      return; 
    }
  }



  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  toggleSidebar() {
    this.isClosed = !this.isClosed;
  }

  navigateToProfile() {
    this.router.navigate(['/dashboarduser/user-profile']); // Navigate to the profile page
    this.isDropdownOpen = true; // Open the dropdown menu
  }

  

  logout() {
    sessionStorage.clear(); // Clear session storage on logout
    this.router.navigate(['/login']);
  }
}