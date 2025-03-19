import { Component, OnInit, AfterViewInit, ViewChild, ElementRef, HostListener } from '@angular/core';
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
  @ViewChild('memberTable', { static: false })
  memberTable!: ElementRef;
  @ViewChild('tableScrollContainer', { static: false })
  tableScrollContainer!: ElementRef;

  constructor(private http: HttpClient, private router: Router) { }

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
          // Remove deleted member instantly from the screen
          this.members = [...this.members.filter(member => member.MemberId !== id)];
        },
        error: (error) => {
          console.error('Error deleting member:', error);
        }
      });
    }
  }

  backToDashboard() {
    this.router.navigate(['/admin-dashboard']);
  }

  setupManualScrolling(): void {
    // No need for cloning rows or setInterval
  }

  @HostListener('wheel', ['$event'])
  onMouseWheel(event: WheelEvent): void {
    if (this.tableScrollContainer) {
      this.tableScrollContainer.nativeElement.scrollTop += event.deltaY;
      event.preventDefault(); // Prevent default page scrolling
    }
  }
}