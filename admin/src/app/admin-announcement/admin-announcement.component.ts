import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-admin-announcement',
  standalone: true, // Required if using module-less approach
  templateUrl: './admin-announcement.component.html',
  imports: [CommonModule, ReactiveFormsModule], // Import required modules
  styleUrls: ['./admin-announcement.component.css']
})
export class AdminAnnouncementComponent implements OnInit {
  announcementForm: FormGroup; // ✅ Declare form property
  announcements: any[] = [];

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router) {
    // ✅ Initialize form inside constructor
    this.announcementForm = this.fb.group({
      announcement_id: [0], // Default to 0 for new announcements
      announcement_name: ['', Validators.required],
      description: ['', Validators.required],
      date: ['', Validators.required] // Ensure correct date format
    });
  }

  ngOnInit() {
    this.loadAnnouncements();
  }

  loadAnnouncements() {
    this.http.get('https://localhost:7256/api/Announcement').subscribe((data: any) => {
      this.announcements = data;
    });
  }

  addAnnouncement() {
    if (this.announcementForm.invalid) return;

    this.http.post('https://localhost:7256/api/Announcement', this.announcementForm.value)
      .subscribe(() => {
        this.loadAnnouncements();
        this.announcementForm.reset();
      });
  }

  backToDashboard() {
    this.router.navigate(['/admin-dashboard']); // ✅ Ensure this route exists
  }
}
