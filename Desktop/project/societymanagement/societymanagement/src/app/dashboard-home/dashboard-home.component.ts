import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-dashboard-home',
  templateUrl: './dashboard-home.component.html',
  styleUrls: ['./dashboard-home.component.css'],
  imports: [CommonModule],
})
export class DashboardHomeComponent implements OnInit {
  
  announcements: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.loadAnnouncement(); 
  }

  loadAnnouncement() {
    this.http.get<any>('http://localhost:7104/api/Announcement/GetAll')
      .subscribe(
        (response: any) => {
          this.announcements = response.data; 
          console.log('Announcements:', this.announcements);
        },
        (error) => {
          console.error('Error fetching announcements:', error);
        }
      );
  }
}
