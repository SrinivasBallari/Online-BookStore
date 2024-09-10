import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  baseUrl: string = 'http://localhost:5187';

  constructor(private http:HttpClient) { }

  getAllOrders(): Observable<any>{
    const token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
    return this.http.get<any>(`${this.baseUrl}/api/Order`, { headers });
  }

  filterOrders(month:number, year:number):  Observable<any> {
      const token = localStorage.getItem('token');
      const headers = new HttpHeaders({
        Authorization: `Bearer ${token}`,
      });
      return this.http.get<any>(`${this.baseUrl}/api/Order/monthly/${month}/${year}`,{headers});
  }
}
