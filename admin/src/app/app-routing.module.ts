import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminLoginComponent } from './admin-login/admin-login.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { AuthGuard } from './guards/auth.guard';
import { AdminEventsComponent } from './admin-events/admin-events.component';
import { AdminAnnouncementComponent } from './admin-announcement/admin-announcement.component';
import { AdminMembersComponent } from './admin-members/admin-members.component';
import { AdminComplaintsComponent } from './admin-complaints/admin-complaints.component';


const routes: Routes = [{
  path:'',redirectTo:'admin-login',pathMatch:'full'},
  {path:'admin-login',component:AdminLoginComponent},
  { path: 'admin-dashboard', component: AdminDashboardComponent, canActivate: [AuthGuard] }, // ✅ Protected Route
  { path: 'admin-events', component: AdminEventsComponent, canActivate: [AuthGuard] },
  {path:'admin-announcement',component:AdminAnnouncementComponent,canActivate:[AuthGuard]},
  {path:'admin-members',component:AdminMembersComponent,canActivate:[AuthGuard]},
  {path:'admin-complaints',component:AdminComplaintsComponent},
  { path: '**', redirectTo: 'admin-login' },
];  


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
