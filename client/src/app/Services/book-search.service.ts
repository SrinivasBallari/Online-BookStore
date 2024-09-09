// src/app/services/book-search.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class BookSearchService {
  private apiUrl = 'http://localhost:5187/api/books'; // Replace with your actual API URL

  constructor(private http: HttpClient) {}

  searchBooks(searchString: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/search/${searchString}`);
  }
}
