import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private apiUrl = 'https://api.example.com/books'; // Replace with your API URL

  constructor(private http: HttpClient) { }

  getBooks(page: number, genres: string[]): Observable<any> {
    let params = new HttpParams().set('page', page.toString());
    
    if (genres.length) {
      params = params.set('genres', genres.join(','));
    }

    return this.http.get<any>(this.apiUrl, { params });
  }
}
