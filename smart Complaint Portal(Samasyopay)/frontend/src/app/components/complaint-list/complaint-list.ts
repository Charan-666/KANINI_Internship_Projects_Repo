import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ComplaintService } from '../../services/complaint';
import { AuthService } from '../../services/auth';
import { Complaint, ComplaintStatus } from '../../models/complaint.model';

@Component({
  selector: 'app-complaint-list',
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './complaint-list.html',
  styleUrl: './complaint-list.scss'
})
export class ComplaintListComponent implements OnInit {
  complaints: Complaint[] = [];
  filteredComplaints: Complaint[] = [];
  loading = true;
  searchTerm = '';
  statusFilter = '';
  userRole: string | null = null;
  
  constructor(
    private complaintService: ComplaintService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.userRole = this.authService.getUserRole();
    this.loadComplaints();
  }

  loadComplaints(): void {
    this.loading = true;
    const userId = this.authService.getUserId();
    const userRole = this.authService.getUserRole();
    
    if (userId) {
      const complaintsObservable = userRole === 'Citizen' 
        ? this.complaintService.getCitizenComplaints(userId)
        : this.complaintService.getAgentComplaints(userId);
        
      complaintsObservable.subscribe({
        next: (complaints) => {
          this.complaints = complaints;
          this.filteredComplaints = complaints;
          this.loading = false;
        },
        error: (error) => {
          console.error('Error loading complaints:', error);
          this.loading = false;
        }
      });
    }
  }

  onSearch(): void {
    this.filterComplaints();
  }

  onStatusFilter(): void {
    this.filterComplaints();
  }

  private filterComplaints(): void {
    let filtered = this.complaints;

    if (this.searchTerm.trim()) {
      const term = this.searchTerm.toLowerCase();
      filtered = filtered.filter(complaint => 
        complaint.title.toLowerCase().includes(term) ||
        complaint.description.toLowerCase().includes(term)
      );
    }

    if (this.statusFilter) {
      const statusValue = this.getStatusValue(this.statusFilter);
      filtered = filtered.filter(complaint => complaint.status === statusValue);
    }

    this.filteredComplaints = filtered;
  }

  getStatusClass(status: any): string {
    switch (Number(status)) {
      case 0: return 'badge bg-warning';
      case 1: return 'badge bg-primary';
      case 2: return 'badge bg-info';
      case 3: return 'badge bg-success';
      case 4: return 'badge bg-danger';
      default: return 'badge bg-secondary';
    }
  }

  getStatusText(status: any): string {
    switch (Number(status)) {
      case 0: return 'Pending';
      case 1: return 'Assigned';
      case 2: return 'In Progress';
      case 3: return 'Resolved';
      case 4: return 'Rejected';
      default: return 'Unknown';
    }
  }

  getStatusValue(statusText: string): number {
    switch (statusText) {
      case 'Pending': return 0;
      case 'Assigned': return 1;
      case 'InProgress': return 2;
      case 'Resolved': return 3;
      case 'Rejected': return 4;
      default: return -1;
    }
  }

  formatDate(date: string | Date): string {
    return new Date(date).toLocaleDateString();
  }

  clearFilters(): void {
    this.searchTerm = '';
    this.statusFilter = '';
    this.filteredComplaints = this.complaints;
  }

  get pendingCount(): number {
    return this.complaints.filter(c => c.status === 0).length;
  }

  get inProgressCount(): number {
    return this.complaints.filter(c => c.status === 2).length;
  }

  get resolvedCount(): number {
    return this.complaints.filter(c => c.status === 3).length;
  }

  get assignedCount(): number {
    return this.complaints.filter(c => c.status === 1).length;
  }
}
