import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FilterService {
  private baseUrl = 'http://localhost:5187'; 

  constructor(private httpclient : HttpClient) {}
 
  getFilters(): Observable<any> {
    return this.httpclient.get(`${this.baseUrl}/api/books/categories`)
    .pipe(
      map((response: any) => {
        return response.$values.map((item: any) => item.tag1);
      })
    );
  }

  getAuthors(): Observable<any> {
    return this.httpclient.get(`${this.baseUrl}/api/books/authors`)
    .pipe(
      map((response: any) => {
        const authors = response.$values.map((item: any) => item.name);
        return Array.from(new Set(authors));
      })
    );
  }
}
