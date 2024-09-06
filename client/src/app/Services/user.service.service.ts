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

  // Get User Details with Authorization Token
  getUserDetails(): Observable<UserProfileResponse> {
    const token = localStorage.getItem('token'); // Get token from localStorage
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}` // Attach the token to Authorization header
    });

    return this.httpClient.get<UserProfileResponse>(`${this.baseUrl}/api/users`, { headers })
      .pipe(
        map((response: any) => {
          // Map the response to return UserProfileResponse
          return response as UserProfileResponse;
        })
      );
  }

  // Update User Details with Authorization Token
  updateUserDetails(updatedField: any): Observable<any> {
    const token = localStorage.getItem('token'); // Get token from localStorage
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}` // Attach the token to Authorization header
    });

    return this.httpClient.post<any>(`${this.baseUrl}/api/users`, updatedField, { headers })
      .pipe(
        map((response: any) => {
          // Handle and map response if needed
          return response;
        })
      );
  }
}
