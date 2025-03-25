import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { error } from 'console';



@Component({
  selector: 'app-flat-transfer',
  standalone: false,
  templateUrl: './flat-transfer.component.html',
  styleUrl: './flat-transfer.component.css'
})
export class FlatTransferComponent {
  Flatdetails: any = {
    CurrentOwnerName: '',  // Changed from 'Currentownername'
    CurrentOwnerContactNumber: '',  // Changed from 'Currentownercontactnumber'
    FlatNumber: '',
    BlockNumber: '',
    FlatArea: '',  // Changed from 'Flatarea'
    NewOwnerFullName: '',
    NewOwnerContactNumber: '',
    NewOwnerEmail: '',
    NewOwnerAdhar: '',
    TransferFees: '', // Changed from 'Transfer_fees' and set as a number
    PaymentMode: '',
    ImageIdentityproofFile: null,
    Agreementcopyfile: null// Changed from 'paymentmode'
  };

  UserId: number | null = null;


  constructor(private authService: AuthService, private router: Router) { }


  // onFileSelected(event: any, field: 'Identityproofurl' | 'Agreementcopyurl') {
  //   const file = event.target.files[0];

  //   if (file) {
  //     // Validate file type
  //     const allowedTypes = ['image/jpeg', 'image/png', 'application/pdf'];
  //     if (!allowedTypes.includes(file.type)) {  
  //       Swal.fire({
  //         title: 'Invalid File Type',
  //         text: 'Please upload a PDF, JPG, or PNG file only.',
  //         icon: 'error',
  //         confirmButtonText: 'Ok'
  //       });
  //       return;
  //     }

  //     // Validate file size (max 5MB)
  //     if (file.size > 5 * 1024 * 1024) {
  //       Swal.fire({
  //         title: 'File Too Large',
  //         text: 'Please upload a file smaller than 5MB.',
  //         icon: 'error',
  //         confirmButtonText: 'Ok'
  //       });
  //       return;
  //     }

  //     this.Flatdetails[field] = file; // Store file object
  //   }
  // }


  onFileSelected(event: any) {
    let file = event.target.files[0];




    if (file) {
      const allowedTypes = ['image/jpeg', 'image/png', 'application/pdf'];
      const maxSize = 5 * 1024 * 1024; // 5MB

      if (!allowedTypes.includes(file.type)) {
        Swal.fire('Invalid File Type', 'Only PDF, JPG, or PNG files are allowed.', 'error');
        return;
      }

      if (file.size > maxSize) {
        Swal.fire('File Too Large', 'Please upload a file smaller than 5MB.', 'error');
        return;
      }

      this.Flatdetails.ImageIdentityproofFile = file; // Assign file
    }
  }

  onFileSelected1(event: any) {
    const file = event.target.files[0];


    if (file) {
      const allowedTypes1 = ['image/jpeg', 'image/png', 'application/pdf'];
      const maxSize1 = 5 * 1024 * 1024; // 5MB

      if (!allowedTypes1.includes(file.type)) {
        Swal.fire('Invalid File Type', 'Only PDF, JPG, or PNG files are allowed.', 'error');
        return;
      }

      if (file.size > maxSize1) {
        Swal.fire('File Too Large', 'Please upload a file smaller than 5MB.', 'error');
        return;
      }

      this.Flatdetails.Agreementcopyfile = file;
    }


  }



  onSubmit(form: any) {
    if (!this.validateForm()) {
      Swal.fire({
        title: 'Validation Error',
        text: 'Please fill in all required fields correctly.',
        icon: 'error',
        confirmButtonText: 'Ok'
      });
      return;
    }


    if (!form.valid) {
      console.log('Form is invalid');
      return;
    }


    const formData = new FormData();
    formData.append('CurrentOwnerName', this.Flatdetails.CurrentOwnerName || '');
    formData.append('CurrentOwnerContactNumber', this.Flatdetails.CurrentOwnerContactNumber || '');
    formData.append('FlatNumber', this.Flatdetails.FlatNumber || '');
    formData.append('BlockNumber', this.Flatdetails.BlockNumber || '');
    formData.append('NewOwnerFullName', this.Flatdetails.NewOwnerFullName || '');
    formData.append('NewOwnerContactNumber', this.Flatdetails.NewOwnerContactNumber || '');
    formData.append('NewOwnerEmail', this.Flatdetails.NewOwnerEmail || '');
    formData.append('NewOwnerAdhar', this.Flatdetails.NewOwnerAdhar || '');
    formData.append('FlatArea', this.Flatdetails.FlatArea || '');
    formData.append('TransferFees', this.Flatdetails.TransferFees.toString()); // Convert decimal to string
    formData.append('PaymentMode', this.Flatdetails.PaymentMode || '');

    if (this.Flatdetails.ImageIdentityproofFile) {
      formData.append('ImageIdentityproofFile', this.Flatdetails.ImageIdentityproofFile);
    }

    if (this.Flatdetails.Agreementcopyfile) {
      formData.append('Agreementcopyfile', this.Flatdetails.Agreementcopyfile);
    }


    // ✅ Debug: Log FormData contents before sending request
    for (const pair of formData.entries()) {
      console.log(pair[0], pair[1]);
    }

    // console.log(this.Flatdetails);
    this.authService.Submit(formData).subscribe({
      next: (response) => {


        console.log("Api response", response);
        if (response && response.success) {
          alert(response.message);// Navigate to success page

        } else {
          Swal.fire({
            title: 'Oops!',
            text: response.message,
            icon: 'error',
            confirmButtonText: 'Ok'
          });
        }
      },
      error: (err) => {
        console.log("Error during API call", err);
        Swal.fire({
          title: 'Oops!',
          text: err.error?.message || "An error occurred",
          icon: 'error',
          confirmButtonText: 'Ok'
        });
      }
    });
  }






  validateForm(): boolean {
    if (!this.Flatdetails.CurrentOwnerName || !this.Flatdetails.CurrentOwnerContactNumber ||
      !this.Flatdetails.FlatNumber || !this.Flatdetails.BlockNumber ||
      !this.Flatdetails.FlatArea || !this.Flatdetails.NewOwnerFullName ||
      !this.Flatdetails.NewOwnerContactNumber || !this.Flatdetails.NewOwnerEmail ||
      !this.Flatdetails.NewOwnerAdhar || !this.Flatdetails.PaymentMode) {
      return false;
    }
    return true;
  }

}

