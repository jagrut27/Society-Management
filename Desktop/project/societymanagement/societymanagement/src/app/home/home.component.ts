import { Component } from '@angular/core';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

constructor(private router:Router ){}

  goToAdmin(){
    window.location.href='http://localhost:64121/admin-login';
  }
}
  