  import { Component, OnInit } from '@angular/core';
  import { AuthService ,complaindata} from '../auth.service';
  import { Router } from '@angular/router';
  import Swal from 'sweetalert2';
import { response } from 'express';


  @Component({
    selector: 'app-complaint',
    standalone: false,
    templateUrl: './complaint.component.html',
    styleUrl: './complaint.component.css'
  })
  export class ComplaintComponent implements OnInit {
    complaintData = {
      title: '',
      description: ''
    };

    complaints:complaindata[] | null=null;

    UserId : number | null = null;


    constructor(private authService: AuthService, private router: Router) { }

    
    ngOnInit(): void {
      // Fetch email from session to display it

      const storedUserId=sessionStorage.getItem('UserId');

      if (storedUserId) {
        this.UserId = parseInt(storedUserId, 10); // Convert string to number
      }

      // this.validateForm();

      this.showcomplain();
    }
    validateForm(): boolean {
      
      if (!this.complaintData.title || !this.complaintData.description) {
        Swal.fire("Error", "All fields are required", "error");
        return false;
      }
      
      return true;
    }



    showcomplain() {
      if (!this.UserId) {
        Swal.fire("Error", "User ID not found. Please login again.", "error");
        return;
      }
    
      console.log("Fetching complaints for UserID:", this.UserId);
    
      this.authService.getcomplainbyuserid(this.UserId).subscribe({
        next: (response) => {
          console.log("Full API Response:", response);
    
          if (response && response.success && response.data) {  
            this.complaints = Array.isArray(response.data) ? response.data : [response.data];
            console.log("Complaints Data:", this.complaints);
          } else {
            console.error("Invalid response structure or missing data.");
          }
        },
        error: (err) => {
          console.error("Error fetching complaints:", err);
        }
      });
    }
    
    

    submitComplaint() {

      if (!this.validateForm()) {
        return; // Stop if validation fails
      }

      
      if (this.UserId === null) {
        Swal.fire("Error", "User ID not found. Please login again.", "error");
        return;
      }
    
      this.authService.submitComplaint(this.UserId, this.complaintData).subscribe({
        next: (response) => {
          console.log(response);
          if (response.success) {
            // Swal.fire({
            //   title: 'Success!',
            //   text: response.message,
            //   icon: 'success',
            //   confirmButtonText: 'OK'
            // });
            alert(response.message);
            window.location.reload();

            // this.complaintData = { title: '', description: '' }; // Reset form
          } 
        },
        error: (err) => {
          alert("Error submitting complaint: " + err.message);
        }
      });
    }
    

  }
