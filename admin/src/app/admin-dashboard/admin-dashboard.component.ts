import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { AdminEventsComponent } from '../admin-events/admin-events.component';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-admin-dashboard',
  standalone: false,
  templateUrl: './admin-dashboard.component.html',
  styleUrl: './admin-dashboard.component.css'
})
export class AdminDashboardComponent {
  complaints: any;

  constructor(private authService: AuthService, private router: Router,private http: HttpClient) {}
  recentComplaints:any[]=[];
  recentPayments: any[] = []; 
  totalMembers: number = 0;
  totalComplaints: number = 0;
  apiURL='https://localhost:7256/api/Complaints/recent';

  ngOnInit(): void {
    this.getRecentComplaints();
    this.getRecentPayments();
    this.getTotalMembers();
    this.getTotalComplaints();
  }
  getTotalComplaints(): void {
    this.http.get<{ totalComplaints: number }>('https://localhost:7256/api/Complaints/count').subscribe(
      data => {
        console.log('Total Complaints:', data.totalComplaints);
        this.totalComplaints = data.totalComplaints;
      },
      error => {
        console.error('Error fetching total complaints:', error);
      }
    );
  }
  

  getRecentComplaints(): void {
    this.http.get(this.apiURL).subscribe(
      (data: any) => {
        this.complaints = data;
      },
      (error) => {
        console.error('Error fetching complaints:', error);
      }
    );
  }
  getRecentPayments(): void {
    this.http.get<any[]>('https://localhost:7256/api/MaintenancePayment/last3days').subscribe(
      data => {
        console.log('Recent Payments:', data);
        this.recentPayments = data;
      },
      error => {
        console.error('Error fetching recent payments:', error);
      }
    );
  }
  getTotalMembers(): void {
    this.http.get<{ totalMembers: number }>('https://localhost:7256/api/Members/count').subscribe(
      data => {
        console.log('Total Members:', data.totalMembers);
        this.totalMembers = data.totalMembers;
      },
      error => {
        console.error('Error fetching total members:', error);
      }
    );
  }

  logout() {
    this.authService.logout();  // ✅ Clear token
    this.router.navigate(['/admin-login']); // ✅ Redirect to login page
  }
  goToEvents() {
    this.router.navigate(['/admin-events']);  // ✅ Redirect to Events Page
  }
  goToComplaints(){
    this.router.navigate(['/admin-complaints']);
  }
  backToDashboard(){
    this.router.navigate(['/admin-dashboard']);
  }
  Announcement(){
    this.router.navigate(['/admin-announcement'])
  }
  goToMembers(){
    this.router.navigate(['/admin-members']);
  }
  gotoMaintenance(){
    this.router.navigate(['/admin-maintenance']);
  }
  FlatTransfer(){
    this.router.navigate(['/admin-flat-transfer'])
  }
 
  sidebarOpen: boolean = false; 
  toggleSidebar() {
    this.sidebarOpen = !this.sidebarOpen;
  }

}
