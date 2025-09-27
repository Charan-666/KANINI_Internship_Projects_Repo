import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { LoginRequest, LoginResponse, RegisterRequest, UserRole } from '../models/user.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${environment.apiUrl}/Auth`;
  private currentUserSubject = new BehaviorSubject<any>(null);
  public currentUser$ = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient) {
    const token = localStorage.getItem('token');
    if (token) {
      this.setCurrentUser();
    }
  }

  login(credentials: LoginRequest): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/login`, credentials)
      .pipe(
        tap(response => {
          localStorage.setItem('token', response.token);
          localStorage.setItem('userId', response.userId.toString());
          localStorage.setItem('userRole', response.role);
          localStorage.setItem('userName', response.userName);
          this.setCurrentUser();
        })
      );
  }

  register(data: RegisterRequest): Observable<any> {
    const formData = new FormData();
    formData.append('Name', data.name);
    formData.append('Email', data.email);
    formData.append('Password', data.password);
    if (data.address) formData.append('Address', data.address);
    if (data.phoneNumber) formData.append('PhoneNumber', data.phoneNumber);
    if (data.photo) formData.append('Photo', data.photo);

    return this.http.post(`${this.apiUrl}/register`, formData);
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
    localStorage.removeItem('userRole');
    localStorage.removeItem('userName');
    this.currentUserSubject.next(null);
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem('token');
  }

  getUserRole(): UserRole | null {
    const role = localStorage.getItem('userRole');
    return role as UserRole || null;
  }

  getUserId(): number | null {
    const userId = localStorage.getItem('userId');
    return userId ? parseInt(userId) : null;
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  setCurrentUser(): void {
    const user = {
      id: this.getUserId(),
      name: localStorage.getItem('userName'),
      role: this.getUserRole()
    };
    this.currentUserSubject.next(user);
  }
}
