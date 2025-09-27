import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AgentService {
  private apiUrl = `${environment.apiUrl}/Agent`;

  constructor(private http: HttpClient) {}

  // Get assigned complaints for agent
  getAssignedComplaints(agentId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/${agentId}/complaints`);
  }

  // Get complaint details
  getComplaintDetails(agentId: number, complaintId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${agentId}/complaints/${complaintId}`);
  }

  // Update complaint status
  updateComplaintStatus(agentId: number, complaintId: number, status: number): Observable<any> {
    return this.http.put(`${this.apiUrl}/${agentId}/complaints/${complaintId}/status?status=${status}`, {});
  }

  // Upload solution document
  uploadSolutionDocument(agentId: number, complaintId: number, file: File): Observable<any> {
    const formData = new FormData();
    formData.append('File', file);
    
    return this.http.post(`${this.apiUrl}/${agentId}/complaints/${complaintId}/solution`, formData);
  }

  // Get complaint types
  getComplaintTypes(): Observable<any[]> {
    return this.http.get<any[]>(`${environment.apiUrl}/ComplaintType`);
  }

  // Download document
  downloadDocument(documentId: number): Observable<Blob> {
    return this.http.get(`${environment.apiUrl}/ComplaintDocument/download/${documentId}`, { responseType: 'blob' });
  }

  // Search complaints
  searchComplaints(agentId: number, filters: any): Observable<any[]> {
    const params = new URLSearchParams();
    if (filters.typeId) params.append('typeId', filters.typeId);
    if (filters.from) params.append('from', filters.from);
    if (filters.to) params.append('to', filters.to);
    if (filters.citizenName) params.append('citizenName', filters.citizenName);
    
    return this.http.get<any[]>(`${this.apiUrl}/${agentId}/complaints/search?${params.toString()}`);
  }
}