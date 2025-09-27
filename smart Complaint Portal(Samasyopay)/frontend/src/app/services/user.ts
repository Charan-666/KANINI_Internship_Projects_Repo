import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  // Citizen Profile Operations
  getCitizenProfile(citizenId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/Citizen/${citizenId}/profile`);
  }

  updateCitizenProfile(citizenId: number, profileData: any): Observable<any> {
    const formData = new FormData();
    formData.append('Address', profileData.address || '');
    formData.append('PhoneNumber', profileData.phoneNumber || '');
    
    if (profileData.photo) {
      formData.append('Photo', profileData.photo);
    }

    return this.http.put(`${this.apiUrl}/Citizen/${citizenId}/profile`, formData);
  }

  // Admin Operations
  getAllUsers(role?: string): Observable<User[]> {
    const params = role ? `?role=${role}` : '';
    return this.http.get<User[]>(`${this.apiUrl}/Admin/users${params}`);
  }

  addAgent(agentData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/Admin/add-agent`, agentData);
  }

  updateAgent(agentId: number, agentData: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/Admin/agents/${agentId}`, agentData);
  }

  deleteAgent(agentId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/Admin/agents/${agentId}`);
  }
}
