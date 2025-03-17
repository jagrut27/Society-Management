  import { HttpClient,HttpHeaders  } from '@angular/common/http';
  import { Injectable } from '@angular/core';
  import { Observable } from 'rxjs';

  export interface userProfile {
    firstname: string;
  lastname: string;
    phoneNumber: string;
    flatNumber: string;
    blockNumber: string;
    imageURL:string;
  

  }

  // export interface updatedata {
  //   firstname: string;
  // lastname: string;
  //   phoneNumber: string;
  //   flatNumber: string;
  //   blockNumber: string;

  //   password:string;
  //   imageURL:string;
  //   imageFile:string;


  // }

  @Injectable({
    providedIn: 'root'
  })
  export class AuthService {

  
    
    private apiurl= "http://localhost:7104/api/Home/addmembers"  //backend url

    private apiurllogin = "http://localhost:7104/authenticateuser"

    private apiurldashboard = "http://localhost:7104/api/home"

    private apiurlcomplain= "http://localhost:7104/api/complain/addcomplain"

    
    // http://localhost:7104/api/home/updateprofile?MemberId=10018

    // http://localhost:7104/api/home/updateprofile?MemberId=10018



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
      delete_profile(memberId: number): Observable<any> {
        return this.http.delete(`${this.apiurldashboard}/DeleteProfile?memberid=${memberId}`);
      }

      //update profile api
      updateProfile(MemberId: number, formData: FormData): Observable<any> {


        return this.http.put(`${this.apiurldashboard}/updateprofile?MemberId=${MemberId}`, formData);
      }

      //submit complain
      submitComplaint(memberId: number, complaintData: any): Observable<any> {
        const body = {
          MemberId: memberId,  // Ensure MemberId is included
          Title: complaintData.title,
          Description: complaintData.description
        };
      
        return this.http.post(this.apiurlcomplain, body);
      }
      
      

  }
