import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { AdminEventsComponent } from '../admin-events/admin-events.component';

@Component({
  selector: 'app-admin-dashboard',
  
  standalone: false,
  templateUrl: './admin-dashboard.component.html',
  styleUrl: './admin-dashboard.component.css'
})
export class AdminDashboardComponent {

  constructor(private authService: AuthService, private router: Router) {}
  logout() {
    this.authService.logout();  // ✅ Clear token
    this.router.navigate(['/admin-login']); // ✅ Redirect to login page
  }
  goToEvents() {
    this.router.navigate(['/admin-events']);  // ✅ Redirect to Events Page
  }

  backToDashboard(){
    this.router.navigate(['/admin-dashboard']);
  }
  Announcement(){
    this.router.navigate(['/admin-announcement'])
  }
 
  sidebarOpen: boolean = false; 
  toggleSidebar() {
    this.sidebarOpen = !this.sidebarOpen;
  }

}
