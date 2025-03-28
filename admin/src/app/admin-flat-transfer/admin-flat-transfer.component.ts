import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-flat-transfer',
  standalone: false,
  templateUrl: './admin-flat-transfer.component.html',
  styleUrl: './admin-flat-transfer.component.css'
})
export class AdminFlatTransferComponent  implements OnInit{
  FlatTransfer:any[]=[]; 
  constructor(private http:HttpClient,private router:Router){}
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
  deleteRequest(){
    
  }

  backToDashboard() {
    this.router.navigate(['/admin-dashboard']); // Ensure this route is correct
  }
}
