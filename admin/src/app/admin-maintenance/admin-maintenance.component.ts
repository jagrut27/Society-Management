import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-maintenance',
  standalone: false,
  templateUrl: './admin-maintenance.component.html',
  styleUrl: './admin-maintenance.component.css'
})
export class AdminMaintenanceComponent {
   maintenacnce : any[]=[]
   

  constructor(private http:HttpClient, private router:Router){}
  ngOnInit() {
    this.loadMaintenance();
  }
  loadMaintenance() {
   
    this.http.get<any[]>('https://localhost:7256/api/MaintenancePayment/all-maintenance-payments')
   
      .subscribe(data => {
        this.maintenacnce = data;
      });
      console.log("Complaints are",this.maintenacnce);
  }

  backToDashboard() {
    this.router.navigate(['/admin-dashboard']); // Ensure this route is correct
  }
}
