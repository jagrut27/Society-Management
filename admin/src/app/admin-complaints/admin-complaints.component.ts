import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
@Component({
  selector: 'app-admin-complaints',
  templateUrl: './admin-complaints.component.html',
  imports:[CommonModule,ReactiveFormsModule],
  styleUrls: ['./admin-complaints.component.css']
})
export class AdminComplaintsComponent implements OnInit {
  complaints: any[] = [];
 // router: any;

  constructor(private http: HttpClient, private router:Router ) {}

  ngOnInit() {
    this.loadComplaints();
  }

  loadComplaints() {
    this.http.get<any[]>('http://localhost:5000/api/complaints')
      .subscribe(data => {
        this.complaints = data;
      });
  }

  updateStatus(id: number, newStatus: string) {
    this.http.put(`http://localhost:5000/api/complaints/${id}`, { status: newStatus })
      .subscribe(() => {
        this.loadComplaints(); // Refresh data
      });
  }

  deleteComplaint(id: number) {
    if (confirm('Are you sure you want to delete this complaint?')) {
      this.http.delete(`http://localhost:5000/api/complaints/${id}`)
        .subscribe(() => {
          this.loadComplaints(); // Refresh data
        });
    }
  }
  backToDashboard() {
    this.router.navigate(['/admin-dashboard']); // Ensure this route is correct
  }
}
