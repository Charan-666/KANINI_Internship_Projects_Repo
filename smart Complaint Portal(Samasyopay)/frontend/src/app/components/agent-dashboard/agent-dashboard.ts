import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { AgentService } from '../../services/agent';
import { AuthService } from '../../services/auth';
import { AdminService } from '../../services/admin';

@Component({
  selector: 'app-agent-dashboard',
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './agent-dashboard.html',
  styleUrl: './agent-dashboard.scss'
})
export class AgentDashboardComponent implements OnInit {
  complaints: any[] = [];
  recentComplaints: any[] = [];
  complaintTypes: any[] = [];
  loading = true;
  currentUser: any;
  activeTab = 'dashboard';
  selectedComplaint: any = null;
  
  stats = {
    total: 0,
    assigned: 0,
    inProgress: 0,
    resolved: 0
  };

  searchFilters = {
    typeId: '',
    from: '',
    to: '',
    citizenName: ''
  };

  constructor(
    private agentService: AgentService,
    private authService: AuthService,
    private adminService: AdminService
  ) {}

  ngOnInit(): void {
    this.authService.currentUser$.subscribe(user => {
      this.currentUser = user;
      if (user?.id) {
        this.loadComplaints();
        this.loadComplaintTypes();
      }
    });
  }

  loadComplaints(): void {
    this.loading = true;
    const agentId = this.authService.getUserId();
    
    if (agentId) {
      this.agentService.getAssignedComplaints(agentId).subscribe({
        next: (complaints) => {
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

  loadComplaintTypes(): void {
    this.agentService.getComplaintTypes().subscribe(types => {
      this.complaintTypes = types;
    });
  }

  setActiveTab(tab: string): void {
    this.activeTab = tab;
  }

  viewComplaintDetails(complaint: any): void {
    console.log('Viewing complaint details:', complaint);
    console.log('Current user:', this.currentUser);
    console.log('Auth token:', this.authService.getToken());
    
    // For now, just show the modal with existing data
    this.selectedComplaint = complaint;
    
    // Uncomment below when API is working
    /*
    const agentId = this.authService.getUserId();
    if (agentId) {
      this.agentService.getComplaintDetails(agentId, complaint.id).subscribe({
        next: (details) => {
          this.selectedComplaint = details;
        },
        error: (error) => {
          console.error('Error loading complaint details:', error);
          this.selectedComplaint = complaint; // Fallback to basic data
        }
      });
    }
    */
  }

  closeModal(): void {
    this.selectedComplaint = null;
  }

  updateComplaintStatus(complaintId: number, status: number): void {
    const agentId = this.authService.getUserId();
    if (agentId) {
      this.agentService.updateComplaintStatus(agentId, complaintId, status).subscribe({
        next: () => {
          this.loadComplaints();
        },
        error: (error) => {
          console.error('Error updating status:', error);
        }
      });
    }
  }

  onStatusChange(event: Event, complaintId: number): void {
    const target = event.target as HTMLSelectElement;
    const status = parseInt(target.value);
    this.updateComplaintStatus(complaintId, status);
  }

  uploadSolution(complaintId: number, event: Event): void {
    const target = event.target as HTMLInputElement;
    const file = target.files?.[0];
    
    if (file) {
      const agentId = this.authService.getUserId();
      if (agentId) {
        this.agentService.uploadSolutionDocument(agentId, complaintId, file).subscribe({
          next: () => {
            this.loadComplaints();
            alert('Solution uploaded successfully!');
          },
          error: (error) => {
            console.error('Error uploading solution:', error);
            alert('Error uploading solution');
          }
        });
      }
    }
  }

  searchComplaints(): void {
    const agentId = this.authService.getUserId();
    if (agentId) {
      this.agentService.searchComplaints(agentId, this.searchFilters).subscribe({
        next: (complaints) => {
          this.complaints = complaints;
          this.calculateStats();
        },
        error: (error) => {
          console.error('Error searching complaints:', error);
        }
      });
    }
  }

  clearSearch(): void {
    this.searchFilters = {
      typeId: '',
      from: '',
      to: '',
      citizenName: ''
    };
    this.loadComplaints();
  }

  calculateStats(): void {
    this.stats.total = this.complaints.length;
    this.stats.assigned = this.complaints.filter(c => c.status === 1).length;
    this.stats.inProgress = this.complaints.filter(c => c.status === 2).length;
    this.stats.resolved = this.complaints.filter(c => c.status === 3).length;
  }

  getStatusClass(status: number): string {
    switch (status) {
      case 0: return 'badge bg-warning';
      case 1: return 'badge bg-primary';
      case 2: return 'badge bg-info';
      case 3: return 'badge bg-success';
      case 4: return 'badge bg-danger';
      default: return 'badge bg-secondary';
    }
  }

  getStatusText(status: number): string {
    switch (status) {
      case 0: return 'Pending';
      case 1: return 'Assigned';
      case 2: return 'In Progress';
      case 3: return 'Resolved';
      case 4: return 'Rejected';
      default: return 'Unknown';
    }
  }

  formatDate(date: string | Date): string {
    return new Date(date).toLocaleDateString();
  }

  downloadDocument(doc: any): void {
    this.agentService.downloadDocument(doc.id).subscribe({
      next: (blob) => {
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = doc.fileName;
        a.click();
        window.URL.revokeObjectURL(url);
      },
      error: (error) => {
        console.error('Error downloading document:', error);
      }
    });
  }
}
