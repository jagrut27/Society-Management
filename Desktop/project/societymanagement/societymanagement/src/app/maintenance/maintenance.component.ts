import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup,FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-maintenance',
  templateUrl: './maintenance.component.html',
  styleUrls: ['./maintenance.component.css'],
  imports :[CommonModule,ReactiveFormsModule]
})
export class MaintenanceComponent implements OnInit {
MaintenanceForm!:FormGroup;
  paymentHistory: any[] = [];
  memberId: number = 0;
  amount:number = 0; // Example maintenance fee
  orderId: string = '';
  user: any; // Holds user details

  constructor(private http: HttpClient,private fb:FormBuilder) {}

  ngOnInit() {
    const storedMemberId = sessionStorage.getItem('UserId');
  
    if (storedMemberId) {
      this.memberId = parseInt(storedMemberId, 10);
      console.log("Retrieved Member ID from session:", this.memberId);
    } else {
      console.error("Member ID not found in sessionStorage, fetching from API...");
    }
    console.log("Calling FetchPaymentHistory...");
    this.FetchPaymentHistory();
      
  
  



this.MaintenanceForm = this.fb.group({
            name: ['', Validators.required],
            email: ['', [Validators.required, Validators.email]],
            FlatNo:['',[Validators.required]],
            Block:['',[Validators.required]],
            amount: ['', [Validators.required, Validators.min(1)]],
            date:['',[Validators.required]],
            paymentMethod: ['', Validators.required]
            
});
  }
  submitPayment() {
 
    if (!this.MaintenanceForm) {
      alert("Form not initialized properly.");
      return;
    }
  
    // Mark all form fields as touched to show validation errors
    this.MaintenanceForm.markAllAsTouched();
    this.MaintenanceForm.updateValueAndValidity();
  
    if (this.MaintenanceForm.invalid) {
      alert("Please fill all required fields before proceeding.");
      return;
    }
  
    // Prepare the payment data
    const paymentData = {
      memberId: this.memberId,
      name: this.MaintenanceForm.value.name,
      email:this.MaintenanceForm.value.email,
      flatNumber: this.MaintenanceForm.value.FlatNo,
      blockNumber: this.MaintenanceForm.value.Block,
      amount: this.MaintenanceForm.value.amount,
      paymentDate: this.MaintenanceForm.value.date,
      paymentMethod: this.MaintenanceForm.value.paymentMethod,
      status: "Pending"
    };
    console.log(paymentData);
    console.log("Submitting Payment Data:", paymentData);
  
    this.http.post('http://localhost:7104/api/Payment/create-order', paymentData)
      .subscribe({
        next: (response) => {
       
          alert("Redirecting to Payment Page!");
  
          this.redirectToRazorpay();
        },
        error: (error) => {
          console.error("Error submitting payment:", error);
          alert("Failed to submit payment. Check console for details.");
        }
      });
  }
  
  
  payNow() {
    if (!this.MaintenanceForm) {
      alert("Form not initialized properly.");
      return;
    }
  
    this.MaintenanceForm.markAllAsTouched();
    this.MaintenanceForm.updateValueAndValidity();
  
    console.log("Form Values:", this.MaintenanceForm.value);
    console.log("Form Valid Status:", this.MaintenanceForm.valid);
  
    if (this.MaintenanceForm.invalid) {
      alert("Please fill all required fields before proceeding.");
      return;
    }
  
    console.log("Form is valid. Proceeding to payment API...");
    this.submitPayment();
  }
  
  
  redirectToRazorpay() {
      window.location.href="https://rzp.io/rzp/6CeAJ9ac";
    };
  
    FetchPaymentHistory() {
      console.log("Fetching payment history for Member ID:", this.memberId);
    
      if (!this.memberId || this.memberId <= 0) {
        console.error("Invalid Member ID. Cannot fetch payment history.");
        return;
      }
    
      this.http.get(`http://localhost:7104/api/Payment/payment-history/${this.memberId}`).subscribe({
        next: (data: any) => {
          console.log("Raw API Response:", data);
    
          this.paymentHistory = data;
          console.log("Updated paymentHistory:", this.paymentHistory);
        },
        error: (error) => {
          console.error("Error fetching payment history:", error);
        }
      });
  }
    
}
    

    
  
   
  


 

