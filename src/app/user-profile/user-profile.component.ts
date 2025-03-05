  import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
  import { AuthService, userProfile } from '../auth.service';
  import { Router } from '@angular/router';

  @Component({
    selector: 'app-user-profile',
    standalone: false,
    templateUrl: './user-profile.component.html',
    styleUrl: './user-profile.component.css'
  })
  export class UserProfileComponent implements OnInit{

    userEmail: string | null = ''; // Ensure it can be null
      Userprofile: userProfile | null = null;// To store the user profile data  
      showProfile: boolean = false; // Toggle profile visibility
      // selectedFile: File | null = null; 
    
      
    
      // member: Member | null = null;
    
      // email: string = '';
    
      constructor(private authService: AuthService, private router: Router, private cd: ChangeDetectorRef ) {}
    
    
    
    
      ngOnInit(): void {
        this.fetchUserProfile(); // Fetch profile data when the component initializes
        this.showProfile = true;  // Ensure Profile is visible when loaded

      }
    
      fetchUserProfile() {
    
        let storedemail = sessionStorage.getItem('userEmail');
        console.log('Logged-in user email:', storedemail); // Log to ensure email is retrieved
    
      
    
        if (!storedemail) {
          console.error('No user email found in session storage.');
          this.router.navigate(['/login']); 
          return; 
        }
    
    
        this.userEmail = storedemail; // Assign to the component variable
    
        // Call the auth service to fetch the user profile data
        this.authService.getMemberByEmail(storedemail).subscribe({
          next: (response) => {
            // console.log('Profile response:', response);
      
            // Extracting `data` property from response
            if (response && response.success && response.data) {
              this.Userprofile = response.data;
            } else {
              console.error('Invalid response structure or missing data.');
            }
          
    
          // console.log('Profile response:', response);
    
          },
          error: (err) => {
            console.error('Error during profile fetch:', err); // Log the error if the request fails
          }
        });
      }
    
      // onFileSelected(event: any) {
      //   if (event.target.files.length > 0) {
      //     this.selectedFile = event.target.files[0]; // Store the selected file
      //   }
      // }
    
    
    
    //   this.authService.uploadImage(memberId, this.selectedFile).subscribe({
    //     next: (response) => {
    //       if (this.Userprofile) {
    //         this.Userprofile.imageUrl = response.imageUrl; // Update the profile image URL
    //       }
    //       alert('Image uploaded successfully!');
    //       this.cd.detectChanges(); // Refresh UI
    //     },
    //     error: (err) => {
    //       console.error('Error uploading image:', err);
    //     }
    //   });
    // }
    
    
      toggleProfile() {
        this.showProfile = !this.showProfile; // Toggle the profile view
    
        // if (this.showProfile) {
        //   this.router.navigate(['/dashboarduser']); // Ensure no child route loads
        // }
      }
    
      logout() {
        // Remove email from sessionStorage and navigate to login page
        sessionStorage.removeItem('userEmail');
        sessionStorage.removeItem('UserId');
        this.router.navigate(['/login']); // Redirect to login page
      }
    }
    

