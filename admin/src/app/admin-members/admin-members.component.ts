import { Component, OnInit, AfterViewInit, ViewChild, ElementRef, HostListener, ChangeDetectorRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-members',
  standalone: false,
  templateUrl: './admin-members.component.html',
  styleUrl: './admin-members.component.css'
})
export class AdminMembersComponent implements OnInit, AfterViewInit {
  members: any[] = [];
  totalMembers: number | undefined;

  @ViewChild('memberTable', { static: false }) memberTable!: ElementRef;
  @ViewChild('tableScrollContainer', { static: false }) tableScrollContainer!: ElementRef;
  

  constructor(private http: HttpClient, private router: Router, private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.getMembers();
  }

  ngAfterViewInit(): void {
    if (this.tableScrollContainer) {
      this.setupManualScrolling();
    }
  }

  getMembers(): void {
    this.http.get<any[]>('https://localhost:7256/api/Members').subscribe(
      data => {
        console.log('Received members data:', data);
        this.members = data;
      },
      error => {
        console.error('Error fetching members:', error);
      }
    );
  }

  deleteMember(id: number): void {
    if (confirm('Are you sure you want to delete this member?')) {
      this.http.delete(`https://localhost:7256/api/Members/${id}`).subscribe({
        next: () => {
          this.members = this.members.filter(member => member.memberId !== id);
          this.cdr.detectChanges(); 
          this.updateTotalMembers();
        },
        error: (error) => {
          console.error('Error deleting member:', error);
        }
      });
    }
  }
  updateTotalMembers(): void {
    this.http.get<{ totalMembers: number }>('https://localhost:7256/api/Members/count').subscribe(
      data => {
        this.totalMembers = data.totalMembers;
      },
      error => {
        console.error('Error updating total members:', error);
      }
  );
}

  backToDashboard() {
    this.router.navigate(['/admin-dashboard']);
  }

  setupManualScrolling(): void {}

  @HostListener('wheel', ['$event'])
  onMouseWheel(event: WheelEvent): void {
    if (this.tableScrollContainer) {
      this.tableScrollContainer.nativeElement.scrollTop += event.deltaY;
      event.preventDefault();
    }
  }
}
