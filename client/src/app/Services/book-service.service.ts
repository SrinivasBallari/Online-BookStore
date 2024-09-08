import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class BookService {
  private baseUrl = 'http://localhost:5187';

  constructor(private http: HttpClient) {}

  getBooks(page: number, pageSize: number, filters: any): Observable<any> {
    let genres = 'none';
    let authors = 'none';
    let genresList = filters?.genres;
    let authorsList = filters?.authors;

    if (genresList.length > 0) {
      genres = genresList.join(',');
    }

    if (authorsList.length > 0) {
      authors = authorsList.join(',');
    }
    return this.http.get<any>(
      `${this.baseUrl}/api/books/${page}/${pageSize}/${genres}/${authors}`
    );
  }

  getBookById(bookId: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/api/books/${bookId}`);
  }

  getSimilarBooks(bookId: number): Observable<any> {
    return this.http.get<any>(
      `${this.baseUrl}/api/books/getSimilarBooks/${bookId}`
    );
  }

  getReviews(bookId: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/api/books/reviews/${bookId}`);
  }

  postReview(rev: {
    bookId: number;
    rating: number;
    review: string;
  }): Observable<any> {
    const token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
    return this.http.post<any>(`${this.baseUrl}/api/books/review`, rev,{headers});
  }
}
