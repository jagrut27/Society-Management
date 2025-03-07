import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface userProfile {
  firstname: string;
lastname: string;
  phoneNumber: string;
  flatNumber: string;
  blockNumber: string;
  // imageUrl?: string;  // Add imageUrl to display the profile image

}

@Injectable({
  providedIn: 'root'
})
export class AuthService {

 
  
  private apiurl= "http://localhost:7104/api/Home/addmembers"  //backend url

  private apiurllogin = "http://localhost:7104/authenticateuser"

  private apiurldashboard = "http://localhost:7104/api/home"

  // private apiurldelete= "http://localhost:7104/api/home/Remove?membetid=3"

  // private apiimageupload="http://localhost:7104/api/image/add"

  constructor(private http: HttpClient) { }

  register(user: any): Observable<any> {
    return this.http.post(this.apiurl , user);
  }

  Login(user1 : any):Observable<any>{
    return this.http.post(this.apiurllogin,user1);
  }


    // // Fetch user by email
    getMemberByEmail(email: string): Observable<{ success: boolean; data: userProfile } > {

      return this.http.get<{ success: boolean; data: userProfile}>(`${this.apiurldashboard}/by-email?email=${email}`);
    }



    // **DELETE Member Profile**
    deleteMember(memberId: number): Observable<any> {
      return this.http.delete(`${this.apiurldashboard}/remove?membetid=${memberId}`);
    }

}
