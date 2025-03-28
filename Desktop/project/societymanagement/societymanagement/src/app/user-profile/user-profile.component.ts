import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService, userProfile,AfterUpdateProfile } from '../auth.service';
import { response } from 'express';
@Component({
  selector: 'app-user-profile',
  standalone: false,
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css'
})

export class UserProfileComponent  implements OnInit {
  userEmail: string | null = '';
  Userprofile: userProfile | AfterUpdateProfile | null = null; // Store either userProfile or AfterUpdateProfile


  UserId : number | null = null;

  constructor(private authService: AuthService, private router: Router, private cd: ChangeDetectorRef) {}

  ngOnInit(): void {
    // Fetch email from session to display it
    console.log("ngOnInit() is running...");
    this.userEmail = sessionStorage.getItem('userEmail');

    const storedUserId=sessionStorage.getItem('UserId');

    console.log('Stored Email:', this.userEmail);
  console.log('Stored UserId:', storedUserId);


    if (storedUserId) {
      this.UserId = parseInt(storedUserId, 10); // Convert string to number
    }

    this.fetchUserProfile();
  }

  fetchUserProfile() {
    if (!this.userEmail) {
      console.error('No user email found in session storage.');
      return;
    }

    if (!this.UserId) {
      console.error('No user id found in session storage.');
      return;
    }

    this.authService.getMemberByEmail(this.userEmail).subscribe({
      next: (response: { success: boolean; data: userProfile }) => {
        console.log('Profile response:', response);

        if (response.success && response.data) {
          this.Userprofile = response.data;
          // if (this.Userprofile.imageURL) {
          //   console.log("Final Corrected Image URL:", this.Userprofile.imageURL);
          // } else {
          //   console.log("No image found for the user.");
          // }
  

          // this.cd.detectChanges(); // Force UI update
        } else {
          console.error('Invalid response structure or missing data.');
        }
      },
      error: (err) => {
        console.error('Error fetching profile:', err);
      }
    });

    
    this.authService.getmemberbyid(this.UserId).subscribe({
      next: (response: { success: boolean; data: AfterUpdateProfile }) => {
        console.log('Profile response:', response);

        if (response.success && response.data) {
          this.Userprofile = { ...this.Userprofile, ...response.data }; // Merge data           
           if (this.Userprofile.imageURL) {
          console.log("Final Corrected Image URL:", this.Userprofile.imageURL);
        } else {
          console.log("No image found for the user.");
        }
  

          // this.cd.detectChanges(); // Force UI update
        } else {
          console.error('Invalid response structure or missing data.');
        }
      },
      error: (err) => {
        console.error('Error fetching profile:', err);
      }
    });
  }

  logOut() {
    // sessionStorage.removeItem('userEmail');
    sessionStorage.clear();
    this.router.navigate(['/login']); // Redirect to login page
    
  }

  Edit_profile(){
    if(this.UserId)
    {
      this.router.navigate([`/edit-profile/${this.UserId}`]);
    }
    else{
      // console.error("User id not found");
      alert("User id not found");
      this.router.navigate([`/login`]);
    }
  }

  delete_profile()
  {

    if (!this.UserId) {
      console.error('User ID not found.');
      return;
    }


    const confirmDelete = confirm('Are you sure you want to delete your profile?');
    if (!confirmDelete) {
      return; // User canceled deletion
    }
      
    this.authService.delete_profile(this.UserId).subscribe({
      next: (response: { success: boolean; message: string }) => { // ✅ Fixed syntax errors
        console.log("Delete profile", response);
    
        if (response.success) {
          alert(response.message);
          this.logOut(); // ✅ Fixed function name (logOut instead of logout)
        } else {
          alert(response.message);  
        }
      },
      error: (err) => { // ✅ Fixed error function syntax
        alert("An error occurred while deleting the profile. Please try again later.");
        console.error("Error deleting profile:", err); // ✅ Added error logging for debugging
      }
    });
  }   
  
}
