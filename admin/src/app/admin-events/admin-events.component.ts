import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-events',
  templateUrl: './admin-events.component.html',
  imports:[CommonModule,ReactiveFormsModule],
  styleUrls: ['./admin-events.component.css']
})
export class AdminEventsComponent implements OnInit {
  events: any[] = [];
  eventForm: FormGroup;
  editingEvent: any = null;

  constructor(private fb: FormBuilder, private http: HttpClient,private router:Router ) {
    this.eventForm = this.fb.group({
     event_name: ['', Validators.required],
      description: ['', Validators.required],
      place:['',Validators.required],
      event_date: ['', Validators.required],
      event_time: ['', Validators.required]
    });
  }

  ngOnInit() {
    this.loadEvents();
  }

  loadEvents() {
    this.http.get('https://localhost:7256/api/Event').subscribe((data: any) => {
      this.events = data;
      console.log("Event id is : ",this.events);
    });
  }

  addEvent() {
    console.log(this.eventForm.value); 
    if (this.eventForm.invalid) return;

    this.http.post('https://localhost:7256/api/Event', this.eventForm.value).subscribe(() => {
      this.loadEvents();
      this.eventForm.reset();
    });
  }

  editEvent(event: any) {
    this.editingEvent = event;
    this.eventForm.patchValue(event); 
    console.log("Events in edit events ",this.events);

  }

  updateEvent() {
    if (!this.editingEvent)  {
      console.error("❌ editingEvent is null or undefined!");
      return;
    }
    console.log("specific event id is : ", this.editingEvent.event_id);
    console.log("Evetns in update event ",this.events);


    this.http.put(`https://localhost:7256/api/Event/${this.editingEvent.event_id}`, this.eventForm.value)
      .subscribe(() => {
        this.loadEvents();
        this.editingEvent = null;
        this.eventForm.reset();
      });
  }

  deleteEvent(id: number) {
    console.log(id);
    this.http.delete(`https://localhost:7256/api/Event/${id}`).subscribe(() => {
      this.loadEvents();
    });  

   
  }
  backToDashboard() {
    this.router.navigate(['/admin-dashboard']); // Ensure this route is correct
  }
}
