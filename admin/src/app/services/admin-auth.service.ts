import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class AdminAuthService {
  private apiUrl='http://localhost:5153/api/AdminLogin/login'

  constructor(private http: HttpClient  ) { }

  login(adminData: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, adminData);
  }
}
