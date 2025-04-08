import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-flat-transfer',
  standalone: false,
  templateUrl: './admin-flat-transfer.component.html',
  styleUrl: './admin-flat-transfer.component.css'
})
export class AdminFlatTransferComponent  implements OnInit{
  FlatTransfer:any[]=[]; 
  constructor(private http:HttpClient,private router:Router,private cdr: ChangeDetectorRef){}
  ngOnInit(): void {
    this.FlatTransferRequest() ;
    throw new Error('Method not implemented.');
  }



  FlatTransferRequest(): void {
    console.log(this.FlatTransfer);

    this.http.get<any[]>('https://localhost:7256/api/FlatTransferRequest').subscribe(
      data => {
        console.log('Received Flat Transfer data:', data);
        this.FlatTransfer = data;
      },
      error => {
        console.error('Error fetching Data:', error);
      }

    );
  }
  deleteRequest(id: number): void {
    if (confirm('Are you sure you want to delete this member?')) {
      this.http.delete(`https://localhost:7256/api/FlatTransferRequest/${id}`).subscribe({
        next: () => {
          this.FlatTransfer = this.FlatTransfer.filter(FlatTransfer => FlatTransfer.transfer_id !== id);
          this.cdr.detectChanges(); 
        //  this.updateTotalMembers();
        this.FlatTransferRequest();
        },
        error: (error) => {
          console.error('Error deleting member:', error);
        }
      });
    }
  }

  backToDashboard() {
    this.router.navigate(['/admin-dashboard']); // Ensure this route is correct
  }
}
