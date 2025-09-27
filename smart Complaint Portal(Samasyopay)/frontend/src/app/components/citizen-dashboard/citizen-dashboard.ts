import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ComplaintService } from '../../services/complaint';
import { AuthService } from '../../services/auth';
import { Complaint, ComplaintStatus } from '../../models/complaint.model';

@Component({
  selector: 'app-citizen-dashboard',
  imports: [CommonModule, RouterLink],
  templateUrl: './citizen-dashboard.html',
  styleUrl: './citizen-dashboard.scss'
})
export class CitizenDashboardComponent implements OnInit {
  complaints: Complaint[] = [];
  recentComplaints: Complaint[] = [];
  loading = true;
  currentUser: any;
  
  stats = {
    total: 0,
    pending: 0,
    assigned: 0,
    inProgress: 0,
    resolved: 0
  };

  constructor(
    private complaintService: ComplaintService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.authService.currentUser$.subscribe(user => {
      this.currentUser = user;
      if (user?.id) {
        this.loadComplaints();
      }
    });
  }

  loadComplaints(): void {
    this.loading = true;
    const citizenId = this.authService.getUserId();
    
    if (citizenId) {
      this.complaintService.getCitizenComplaints(citizenId).subscribe({
        next: (complaints) => {
          console.log('Raw complaints data:', complaints);
          complaints.forEach((c, i) => {
            console.log(`Complaint ${i}:`, {
              id: c.id,
              title: c.title,
              status: c.status,
              statusType: typeof c.status
            });
          });
          this.complaints = complaints;
          this.calculateStats();
          this.recentComplaints = complaints.slice(0, 5);
          this.loading = false;
        },
        error: (error) => {
          console.error('Error loading complaints:', error);
          this.loading = false;
        }
      });
    }
  }

  calculateStats(): void {
    this.stats.total = this.complaints.length;
    this.stats.pending = this.complaints.filter(c => c.status === 0).length;
    this.stats.assigned = this.complaints.filter(c => c.status === 1).length;
    this.stats.inProgress = this.complaints.filter(c => c.status === 2).length;
    this.stats.resolved = this.complaints.filter(c => c.status === 3).length;
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
    console.log('Citizen Dashboard - Status value received:', status, typeof status);
    switch (Number(status)) {
      case 0: return 'Pending';
      case 1: return 'Assigned';
      case 2: return 'In Progress';
      case 3: return 'Resolved';
      case 4: return 'Rejected';
      default: return `Unknown (${status})`;
    }
  }

  formatDate(date: string | Date): string {
    return new Date(date).toLocaleDateString();
  }
}
