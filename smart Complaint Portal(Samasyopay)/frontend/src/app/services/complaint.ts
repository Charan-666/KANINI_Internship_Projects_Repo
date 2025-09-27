import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Complaint, CreateComplaintRequest, ComplaintType } from '../models/complaint.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ComplaintService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  // Citizen Operations
  createComplaint(citizenId: number, complaint: CreateComplaintRequest): Observable<any> {
    const formData = new FormData();
    formData.append('Title', complaint.title);
    formData.append('Description', complaint.description);
    formData.append('ComplaintTypeId', complaint.complaintTypeId.toString());
    
    if (complaint.documents) {
      complaint.documents.forEach(doc => {
        formData.append('Documents', doc);
      });
    }

    return this.http.post(`${this.apiUrl}/Citizen/${citizenId}/complaints`, formData);
  }

  getCitizenComplaints(citizenId: number): Observable<Complaint[]> {
    return this.http.get<Complaint[]>(`${this.apiUrl}/Citizen/${citizenId}/complaints`);
  }

  updateComplaint(complaintId: number, updateData: any): Observable<any> {
    const formData = new FormData();
    formData.append('ComplaintId', complaintId.toString());
    formData.append('Title', updateData.title);
    formData.append('Description', updateData.description);
    formData.append('ComplaintTypeId', updateData.complaintTypeId.toString());
    
    const citizenId = updateData.citizenId || 1;
    return this.http.put(`${this.apiUrl}/Citizen/${citizenId}/complaints`, formData);
  }

  deleteComplaint(citizenId: number, complaintId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/Citizen/${citizenId}/complaints/${complaintId}`);
  }

  // Agent Operations
  getAgentComplaints(agentId: number): Observable<Complaint[]> {
    return this.http.get<Complaint[]>(`${this.apiUrl}/Complaint/agent/${agentId}`);
  }

  updateComplaintStatus(agentId: number, complaintId: number, status: number, solutionFile?: File | null): Observable<any> {
    const url = `${this.apiUrl}/Agent/${agentId}/complaints/${complaintId}/status?status=${status}`;
    
    if (solutionFile) {
      // Upload solution file separately
      this.uploadSolutionDocument(agentId, complaintId, solutionFile).subscribe();
    }
    
    return this.http.put(url, {});
  }

  uploadSolutionDocument(agentId: number, complaintId: number, file: File): Observable<any> {
    const formData = new FormData();
    formData.append('File', file);
    return this.http.post(`${this.apiUrl}/Agent/${agentId}/complaints/${complaintId}/solution`, formData);
  }



  // Admin Operations
  getAllComplaints(): Observable<Complaint[]> {
    return this.http.get<Complaint[]>(`${this.apiUrl}/Admin/complaints`);
  }

  getComplaintDetails(complaintId: number): Observable<Complaint> {
    return this.http.get<Complaint>(`${this.apiUrl}/Admin/complaints/${complaintId}`);
  }

  assignComplaint(complaintId: number, agentId: number): Observable<any> {
    return this.http.put(`${this.apiUrl}/Admin/complaints/${complaintId}/assign/${agentId}`, {});
  }

  searchComplaints(searchParams: any): Observable<Complaint[]> {
    return this.http.get<Complaint[]>(`${this.apiUrl}/Admin/complaints/search`, { params: searchParams });
  }

  // Shared Operations
  getComplaintTypes(): Observable<ComplaintType[]> {
    return this.http.get<ComplaintType[]>(`${this.apiUrl}/ComplaintType`);
  }

  downloadDocument(documentId: number): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/ComplaintDocument/download/${documentId}`, {
      responseType: 'blob'
    });
  }

  uploadDocument(complaintId: number, file: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', file);
    return this.http.post(`${this.apiUrl}/ComplaintDocument/upload/${complaintId}`, formData);
  }


}
