import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService, userProfile } from '../auth.service'; // Make sure the AuthService is correctly imported
import { event } from 'jquery';

@Component({
  selector: 'app-userdashboard',
  standalone: false,
  templateUrl: './userdashboard.component.html',
  styleUrls: ['./userdashboard.component.css']
})
export class UserdashboardComponent implements OnInit {
  constructor(private router: Router) {}

  ngOnInit(): void {
    let storedEmail = sessionStorage.getItem('userEmail');
    console.log('Logged-in user email:', storedEmail); // Log to ensure email is retrieved

    if (!storedEmail) {
      console.error('No user email found in session storage.');
      this.router.navigate(['/login']); 
      return; 
    }
  }
}