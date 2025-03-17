import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-admin-announcement',
  standalone: true,
  templateUrl: './admin-announcement.component.html',
  imports: [CommonModule, ReactiveFormsModule],
  styleUrls: ['./admin-announcement.component.css']
})
export class AdminAnnouncementComponent implements OnInit {
  announcementForm: FormGroup;
  announcements: any[] = [];
  editingAnnouncement: any = null; // Track selected announcement for editing

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router) {
    this.announcementForm = this.fb.group({
      announcement_id: [0], 
      announcement_name: ['', Validators.required],
      description: ['', Validators.required],
      date: ['', Validators.required] 
    });
  }

  ngOnInit() {
    this.loadAnnouncements();
  }

  // Load announcements from API
  loadAnnouncements() {
    this.http.get('https://localhost:7256/api/Announcement')
      .subscribe((data: any) => {
        this.announcements = data;
        console.log(data);
      });
  }

  // Add new announcement
  addAnnouncement() {
    if (this.announcementForm.invalid) return;

    this.http.post('https://localhost:7256/api/Announcement', this.announcementForm.value)
      .subscribe(() => {
        this.loadAnnouncements();
        this.announcementForm.reset();
      });
  }

  //get announcemnt data in form back after clickin edit 
  startEditing(announcement: any) {
    this.editingAnnouncement = announcement;
    this.announcementForm.patchValue({
      announcement_id: announcement.announcement_id,
      announcement_name: announcement.announcement_name,
      description: announcement.description,
      date: announcement.date
    });
  }

  // Update announcement
  editAnnouncement() {
    if (!this.editingAnnouncement) return;

    this.http.put(`https://localhost:7256/api/Announcement/${this.editingAnnouncement.announcement_id}`, this.announcementForm.value)
      .subscribe(() => {
        console.log('Announcement updated successfully');
        this.loadAnnouncements();
        this.editingAnnouncement = null; // Reset editing state
        this.announcementForm.reset();
      });
  }

  // Delete announcement
  deleteAnnouncement(id: number) {
    this.http.delete(`https://localhost:7256/api/Announcement/${id}`)
      .subscribe(() => {
        console.log('Announcement deleted successfully');
        this.loadAnnouncements();
      });
  }

  // Navigate back to dashboard
  backToDashboard() {
    this.router.navigate(['/admin-dashboard']);
  }
}
