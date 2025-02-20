import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration, withEventReplay } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormGroup, ReactiveFormsModule } from '@angular/forms'; 
import { AdminLoginComponent } from './admin-login/admin-login.component';
import { HttpClientModule} from '@angular/common/http';
import { CommonModule } from '@angular/common';

import { importProvidersFrom } from '@angular/core';

@NgModule({
  declarations: [
    AppComponent
  
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    
    
    AdminLoginComponent,  
    CommonModule
  ],
  providers: [
  //  importProvidersFrom(AdminLoginComponent),
    provideClientHydration(withEventReplay())
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
