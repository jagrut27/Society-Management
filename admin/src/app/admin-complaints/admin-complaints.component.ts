import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
@Component({
  selector: 'app-admin-complaints',
  templateUrl: './admin-complaints.component.html',
  imports:[CommonModule,ReactiveFormsModule],
  styleUrls: ['./admin-complaints.component.css']
})
export class AdminComplaintsComponent implements OnInit {
  complaints: any[] = [];
  totalComplaints:number | undefined ;
 // router: any;
   apiUrl='https://localhost:7256/api/Complaints/UpdateComplaintStatus';
   apiURL='https://localhost:7256/api/Complaints/recent';

  constructor(private http: HttpClient, private router:Router ) {}

  ngOnInit() {
    this.loadComplaints();
  }

  loadComplaints() {
   
    this.http.get<any[]>('https://localhost:7256/api/Complaints')
   
      .subscribe(data => {
        this.complaints = data;
      });
      console.log("Complaints are",this.complaints);
  }
  getRecentComplaints(): Observable<any> {
    return this.http.get<any>(this.apiURL);
  }

  updateStatus(complaintId: number, newStatus: string) {
    this.http.put(`https://localhost:7256/api/Complaints/${complaintId}`, { status: newStatus })
      .subscribe(() => {
        console.log(`Complaint ${complaintId} updated to ${newStatus}`);
        this.complaints = this.complaints.map(complaint =>
          complaint.complaints_id === complaintId ? { ...complaint, status: newStatus } : complaint
        );
      }, error => {
        console.error('Error updating status:', error);
      });
  }
  

  deleteComplaint(id: number) {
    console.log(id);
    if (confirm('Are you sure you want to delete this complaint?')) {
      this.http.delete(`https://localhost:7256/api/Complaints/${id}`)
    
      
        .subscribe(() => {
          this.loadComplaints(); // Refresh data
          this.updateTotalComplaints();
        });
    }
  }
  updateTotalComplaints(): void {
    this.http.get<{ totalComplaints: number }>('https://localhost:7256/api/Complaints/count').subscribe(
      data => {
        this.totalComplaints = data.totalComplaints;
      },
      error => {
        console.error('Error updating total complaints:', error);
      }
    );
  }
  toggleStatus(complaint: any) {
    const newStatus = complaint.status.trim().toLowerCase() === 'pending' ? 'Resolved' : 'Pending';

    this.http.put(this.apiUrl, { complaints_id: complaint.complaints_id, status: newStatus }).subscribe(response => {
      // If update is successful, change status in UI
      complaint.status = newStatus;
    });
  }
  

  backToDashboard() {
    this.router.navigate(['/admin-dashboard']); // Ensure this route is correct
  }
}
