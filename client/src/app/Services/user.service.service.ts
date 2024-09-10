import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { UserProfileResponse } from '../Models/user-profile-response.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl: string = 'http://localhost:5187';

  constructor(private httpClient: HttpClient) {}

  getUserDetails(): Observable<UserProfileResponse> {
    const token = localStorage.getItem('token'); 
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.httpClient.get<UserProfileResponse>(`${this.baseUrl}/api/users`, { headers })
      .pipe(
        map((response: any) => {
          return response as UserProfileResponse;
        })
      );
  }

  updateUserDetails(updatedField: any): Observable<any> {
    const token = localStorage.getItem('token'); 
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.httpClient.post<any>(`${this.baseUrl}/api/users`, updatedField, { headers })
      .pipe(
        map((response: any) => {
          return response;
        })
      );
  }

  getOrdersOfUser(email : string):Observable<any>{
    const token = localStorage.getItem('token'); 
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.httpClient.get<any>(`${this.baseUrl}/api/Order/Email/${email}`,{ headers });
  }
}
