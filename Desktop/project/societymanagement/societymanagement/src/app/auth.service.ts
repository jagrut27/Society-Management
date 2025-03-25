  import { HttpClient,HttpHeaders  } from '@angular/common/http';
  import { Injectable } from '@angular/core';
  import { map, Observable } from 'rxjs';

  export interface userProfile {
    firstname: string;
  lastname: string;
    phoneNumber: string;
    flatNumber: string;
    blockNumber: string;
    imageURL:string;
  

  }

  export interface complaindata{

    title:string;
    description:string;
    status:string;
    created_at:Date;
  }


  export interface AfterUpdateProfile extends userProfile {
    imageURL: string;
  }

  @Injectable({
    providedIn: 'root'
  })
  export class AuthService {

    // http://localhost:7104/api/home/addmembers

    
    private apiurl= "http://localhost:7104/api/Home/addmembers"  //backend url

    private apiurllogin = "http://localhost:7104/authenticateuser"

    private apiurldashboard = "http://localhost:7104/api/home"

    private apiurlcomplain= "http://localhost:7104/api/complain/addcomplain"





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

      
      getmemberbyid(id:number):Observable<{ success: boolean; data: AfterUpdateProfile } > {

        return this.http.get<{ success: boolean; data: AfterUpdateProfile}>(`${this.apiurldashboard}/by-id?memberid=${id}`);
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

  

      getcomplainbyuserid(userId: number) {
        return this.http.get<any>(`http://localhost:7104/api/complain/complainby-id`, {
          params: { memberid: userId.toString() }
        });
      }

      GetEventbyuser() {
        return this.http.get<any>(`http://localhost:7104/api/Event`);
      }
      


  // Method to submit flat transfer details
  Submit(Flatdetails:any): Observable<any> {
    return this.http.post('http://localhost:7104/Api/FlatTransfer', Flatdetails);


  }
  }
