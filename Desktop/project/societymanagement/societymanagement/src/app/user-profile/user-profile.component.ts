import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService, userProfile } from '../auth.service';
@Component({
  selector: 'app-user-profile',
  standalone: false,
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css'
})

export class UserProfileComponent  implements OnInit {
  userEmail: string | null = '';
  Userprofile: userProfile | null = null; // Initially null to avoid unnecessary API calls

  constructor(private authService: AuthService, private router: Router, private cd: ChangeDetectorRef) {}

  ngOnInit(): void {
    // Fetch email from session to display it
    this.userEmail = sessionStorage.getItem('userEmail');

    this.fetchUserProfile();
  }

  fetchUserProfile() {
    if (!this.userEmail) {
      console.error('No user email found in session storage.');
      return;
    }

    this.authService.getMemberByEmail(this.userEmail).subscribe({
      next: (response: { success: boolean; data: userProfile }) => {
        console.log('Profile response:', response);

        if (response.success && response.data) {
          this.Userprofile = response.data;
          this.cd.detectChanges(); // Force UI update
        } else {
          console.error('Invalid response structure or missing data.');
        }
      },
      error: (err) => {
        console.error('Error fetching profile:', err);
      }
    });
  }

  logout() {
    sessionStorage.removeItem('userEmail');
    this.router.navigate(['/login']); // Redirect to login page
  }

}
