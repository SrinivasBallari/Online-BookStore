import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterRequest } from '../Models/register-request.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { AuthResponse } from '../Models/auth-response.model';
import { LoginRequest } from '../Models/login-request.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl : string = 'http://localhost:5187';
  private loggedIn = new BehaviorSubject<boolean>(this.hasToken()); 
  public isAdminTemp = new BehaviorSubject<boolean>(false);

  constructor(private http : HttpClient, private router:Router) {
  }

  private hasToken(): boolean {
    return !!localStorage.getItem('token');
  }

  get isLoggedIn(): Observable<boolean> {
    return this.loggedIn.asObservable();
  }

  get isUserAdmin() : Observable<boolean> {
    return this.isAdminTemp.asObservable();
  }

  register(userData : RegisterRequest) : Observable<AuthResponse>{
    return this.http.post<AuthResponse>(`${this.baseUrl}/Auth/register`,userData);
  }

  login(credentials: LoginRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.baseUrl}/Auth/login`, credentials);
  }

  isAdmin():Observable<any> {
    const token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
    return this.http.get<any>(`${this.baseUrl}/Auth/verifyUserRole`,{headers});
  }

  setTokenAndNavigateToHome(token: string): void {
    localStorage.setItem('token', token);
    this.loggedIn.next(true);
    // this.isAdminTemp.next(true);
    this.isAdmin().subscribe((response) => {
      if(response.message == "true"){
        this.isAdminTemp.next(true);
      }
    })
    this.router.navigate(['']);
  }

  logout(): void {
    localStorage.removeItem('token');
    this.loggedIn.next(false);
    this.isAdminTemp.next(false);
  }
}
