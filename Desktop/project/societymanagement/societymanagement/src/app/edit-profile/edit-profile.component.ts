import { Component, OnInit } from '@angular/core';
import { AuthService, userProfile } from '../auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit-profile',
  standalone: false,
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.css'
})
export class EditProfileComponent implements OnInit {
  updatedata : any = {
    firstname: '',
    lastname: '',
    phoneNumber: '',
    flatNumber: '',
    blockNumber: '',
    // newPassword: '',
    imageFile: null
  };

  // Updatedata: updatedata | null = null;
  UserId : number | null = null;

  constructor(private authService: AuthService, private router: Router) {}



  ngOnInit(): void{
    let storedEmail = sessionStorage.getItem('userEmail');
    console.log('Logged-in user email:', storedEmail); // Log to ensure email is retrieved

       const storedUserId=sessionStorage.getItem('UserId');

       if (storedUserId) {
        this.UserId = parseInt(storedUserId, 10); // Convert string to number
      }
  

    if (!storedEmail) {
      console.error('No user email found in session storage.');
      this.router.navigate(['/login']); 
      return; 
    }

    this.authService.getMemberByEmail(storedEmail).subscribe({
      next: (response: { success: boolean; data: userProfile }) => {
        console.log('Profile response:', response);

        if (response.success && response.data) {
          this.updatedata = { ...response.data }; 
        } else {
          console.error('Invalid response structure or missing data.');
        }
      },
      error: (err) => {
        console.error('Error fetching profile:', err);
      }

    });


  }



  onFileSelected(event: any) {
    const file = event.target.files[0];
    // this.updatedata.imageFile = file; 
    if (file) {
      this.updatedata.imageFile = file;
    }
  }

  updateProfile() {
    if (!this.UserId) {
      console.error('Member ID not available.');
      return;
    }


  
    const formData = new FormData();
    formData.append('memberId', this.UserId.toString()); // ✅ Ensure MemberId is included
    formData.append('firstname', this.updatedata.firstname || '');
    formData.append('lastname', this.updatedata.lastname || '');
    // formData.append('email', this.updatedata.email || '');
    // formData.append('password', this.updatedata.newPassword || '');
    formData.append('phoneNumber', this.updatedata.phoneNumber || '');
    formData.append('flatNumber', this.updatedata.flatNumber || '');
    formData.append('blockNumber', this.updatedata.blockNumber || '');

    
    if (this.updatedata.imageFile) {
      formData.append('imageFile', this.updatedata.imageFile);
    }
  
    // ✅ Debug: Log FormData contents before sending request
    for (const pair of formData.entries()) {
      console.log(pair[0], pair[1]);
    }
  
    this.authService.updateProfile(this.UserId,formData).subscribe({
      next: (response: { success: boolean; message: string }) => {
        console.log('Server Response:', response);
        if (response && response.success) {
          alert(response.message || 'Profile updated successfully');
          this.router.navigate(['dashboarduser']);
        } else {
          alert('Profile update failed.');
        }
      },
      error: (err) => {
        console.error('Error updating profile:', err);
        if (err.error && err.error.errors) {
          console.error('Validation Errors:', err.error.errors);
        }
        alert('An error occurred. Check the console for details.');
      }
    });

   


  }
  
  cancelEdit() {
    this.router.navigate(['/dashboarduser']);
  }

}
