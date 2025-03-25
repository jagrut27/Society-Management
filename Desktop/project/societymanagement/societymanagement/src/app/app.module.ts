import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration, withEventReplay } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserRegistrationComponent } from './user-registration/user-registration.component';

import { FormControlName, FormsModule } from '@angular/forms';

import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';  // Add this for mat-card components
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { HttpClientModule } from '@angular/common/http';

import { AuthService } from './auth.service';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { UserdashboardComponent } from './userdashboard/userdashboard.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { MaintenanceComponent } from './maintenance/maintenance.component';
import { ForgetPasswordComponent } from './forget-password/forget-password.component';
import { ComplaintComponent } from './complaint/complaint.component';
import { ReactiveFormsModule } from '@angular/forms';  // Keep this line
import { EventComponent } from './event/event.component'; // Keep this line
import { FlatTransferComponent } from './flat-transfer/flat-transfer.component';

@NgModule({
  declarations: [
    AppComponent,
    UserRegistrationComponent,
    HomeComponent,
    LoginComponent,
    UserdashboardComponent,
    UserProfileComponent,
    EditProfileComponent,
    ForgetPasswordComponent,
    ComplaintComponent,
    EventComponent, // Keep this
    FlatTransferComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatButtonModule,
    MatToolbarModule,
    MatCardModule, // Make sure MatCardModule is included
    MatInputModule,
    MatFormFieldModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule // Keep this
  ],
  providers: [
    provideClientHydration(withEventReplay())
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
