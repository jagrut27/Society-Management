import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration, withEventReplay } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormGroup, ReactiveFormsModule } from '@angular/forms'; 
import { AdminLoginComponent } from './admin-login/admin-login.component';
import { HttpClientModule} from '@angular/common/http';
//import { CommonModule } from '@angular/common';

//import { importProvidersFrom } from '@angular/core';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { AdminEventsComponent } from './admin-events/admin-events.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    AppComponent,
    AdminDashboardComponent,
   // 
  
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    
    RouterModule.forRoot([]) ,
    AdminLoginComponent, 
    AdminEventsComponent, 
  //  CommonModule
  ],
  providers: [
  //  importProvidersFrom(AdminLoginComponent),
    provideClientHydration(withEventReplay())
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
