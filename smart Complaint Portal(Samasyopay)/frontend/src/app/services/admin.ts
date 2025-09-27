import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private apiUrl = `${environment.apiUrl}/Admin`;

  constructor(private http: HttpClient) {}

  // User Management
  getAllUsers(role?: string): Observable<any[]> {
    const params = role ? `?role=${role}` : '';
    return this.http.get<any[]>(`${this.apiUrl}/users${params}`);
  }

  addAgent(agentData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/add-agent`, agentData);
  }

  updateAgent(agentId: number, agentData: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/agents/${agentId}`, agentData);
  }

  deleteAgent(agentId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/agents/${agentId}`);
  }

  deleteUser(userId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/users/${userId}`);
  }

  // Complaint Management
  getAllComplaints(): Observable<any[]> {
    return this.http.get<any[]>(`${environment.apiUrl}/Complaint/all`);
  }

  getComplaintDetails(complaintId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/complaints/${complaintId}`);
  }

  assignComplaint(complaintId: number, agentId: number): Observable<any> {
    return this.http.put(`${this.apiUrl}/complaints/${complaintId}/assign/${agentId}`, {});
  }

  searchComplaints(filters: any): Observable<any[]> {
    const params = new URLSearchParams();
    if (filters.typeId) params.append('typeId', filters.typeId);
    if (filters.from) params.append('from', filters.from);
    if (filters.to) params.append('to', filters.to);
    if (filters.status) params.append('status', filters.status);
    if (filters.agentId) params.append('agentId', filters.agentId);
    if (filters.citizenId) params.append('citizenId', filters.citizenId);
    
    return this.http.get<any[]>(`${this.apiUrl}/complaints/search?${params.toString()}`);
  }

  // Complaint Type Management
  getAllComplaintTypes(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/complaint-types`);
  }

  createComplaintType(typeData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/complaint-types`, typeData);
  }

  updateComplaintType(id: number, typeData: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/complaint-types/${id}`, typeData);
  }

  deleteComplaintType(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/complaint-types/${id}`);
  }
}