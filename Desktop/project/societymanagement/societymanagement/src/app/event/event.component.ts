import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { response } from 'express';

@Component({
  selector: 'app-event',
  standalone: false,
  templateUrl: './event.component.html',
  styleUrl: './event.component.css'
})
export class EventComponent implements OnInit {
  events:any[]=[];

   constructor(private authService: AuthService, private router: Router) { }
  ngOnInit(): void {
    this.showevent();
  }

  showevent() {
    this.authService.GetEventbyuser().subscribe({
      next: (data) => { 
        // console.log('Received events data:', data);
        this.events = Array.isArray(data) ? data : [];  // ✅ Ensure it's an array
      },
      error: (err) => {
        console.error("Error Fetching Events", err);
      }
    });

  }
  // showevent() {
  //   this.authService.GetEventbyuser().subscribe({
  //     next: (events) => {
  //       // console.log('Received events data:', events);
  //       this.events = events; // Directly assign the extracted array
  //     },
  //     error: (err) => {
  //       console.error("Error Fetching Events", err);
  //     }
  //   });
  // }
  
  
 
}
