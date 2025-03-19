import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserRegistrationComponent } from './user-registration/user-registration.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { UserdashboardComponent } from './userdashboard/userdashboard.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { MaintenanceComponent } from './maintenance/maintenance.component';
import { ForgetPasswordComponent } from './forget-password/forget-password.component';
import { ComplaintComponent } from './complaint/complaint.component';


const routes: Routes = [
  { path: '', component: HomeComponent },
  
  { path: 'user-registration', component: UserRegistrationComponent },
  { path: 'login', component: LoginComponent },
  
  {path: 'dashboarduser', component: UserdashboardComponent,
    children: [
      { path: 'maintenance', component: MaintenanceComponent },
      { path: 'user-profile', component: UserProfileComponent },
      { path: 'complaints', component: ComplaintComponent },
 //     {path:'admin-login',component:AdminloginComponent}

    ]
  },

  { path: 'edit-profile/:memberId', component: EditProfileComponent },

  { path: 'forget-pass', component: ForgetPasswordComponent },

  

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
