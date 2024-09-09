import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private baseUrl = 'http://localhost:5187';

  constructor(private http: HttpClient) {}

  fetchCartData(): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
    return this.http.get<any>(`${this.baseUrl}/api/Cart`, { headers });
  }

  removeItemFromCart(bookId: number): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
    return this.http.delete<any>(`${this.baseUrl}/api/Cart/${bookId}`, {
      headers,
    });
  }

  reduceItemQuantity(bookId: number): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
    return this.http.put<any>(
      `${this.baseUrl}/api/Cart/${bookId}`,
      {},
      { headers }
    );
  }

  addToCart(bookId: number): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
    return this.http.post<any>(
      `${this.baseUrl}/api/Cart/${bookId}`,
      {},
      { headers }
    );
  }

  placeOrder(selectedPaymentMethod: string): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
    return this.http.post<any>(
      `${this.baseUrl}/api/Order`,
      { paymentType: selectedPaymentMethod },
      { headers }
    );
  }
}
