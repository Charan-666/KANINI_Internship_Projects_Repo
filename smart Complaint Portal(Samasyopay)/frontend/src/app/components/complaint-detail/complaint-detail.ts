import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ComplaintService } from '../../services/complaint';
import { AuthService } from '../../services/auth';
import { Complaint, ComplaintType } from '../../models/complaint.model';

@Component({
  selector: 'app-complaint-detail',
  imports: [CommonModule, FormsModule],
  templateUrl: './complaint-detail.html',
  styleUrl: './complaint-detail.scss'
})
export class ComplaintDetailComponent implements OnInit {
  complaint: Complaint | null = null;
  complaintTypes: ComplaintType[] = [];
  loading = true;
  isEditing = false;
  saving = false;
  error = '';
  success = '';

  editData = {
    title: '',
    description: '',
    complaintTypeId: 0
  };
  
  agentData = {
    status: 0,
    solutionFile: null as File | null
  };
  
  userRole: string | null = null;
  isAgentUpdate = false;
  


  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private complaintService: ComplaintService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    const complaintId = Number(this.route.snapshot.paramMap.get('id'));
    const userId = this.authService.getUserId();
    this.userRole = this.authService.getUserRole();
    this.isAgentUpdate = this.route.snapshot.url.some(segment => segment.path === 'update');
    
    if (complaintId && userId) {
      this.loadComplaint(userId, complaintId, this.userRole);
      this.loadComplaintTypes();
    }
  }

  loadComplaint(userId: number, complaintId: number, userRole: string | null): void {
    this.loading = true;
    const complaintsObservable = userRole === 'Citizen' 
      ? this.complaintService.getCitizenComplaints(userId)
      : this.complaintService.getAgentComplaints(userId);
      
    complaintsObservable.subscribe({
      next: (complaints) => {
        this.complaint = complaints.find(c => c.id === complaintId) || null;
        if (this.complaint) {
          this.editData = {
            title: this.complaint.title,
            description: this.complaint.description,
            complaintTypeId: this.complaint.complaintTypeId
          };
          this.agentData.status = this.complaint.status;
        }
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading complaint:', error);
        this.loading = false;
      }
    });
  }

  loadComplaintTypes(): void {
    this.complaintService.getComplaintTypes().subscribe({
      next: (types) => {
        this.complaintTypes = types;
      },
      error: (error) => {
        console.error('Error loading complaint types:', error);
      }
    });
  }

  toggleEdit(): void {
    this.isEditing = !this.isEditing;
    this.error = '';
    this.success = '';
  }

  saveChanges(): void {
    if (!this.complaint || !this.validateForm()) {
      return;
    }

    this.saving = true;
    this.error = '';
    const citizenId = this.authService.getUserId();

    if (citizenId) {
      const updateData = {
        complaintId: this.complaint.id,
        citizenId: citizenId,
        ...this.editData
      };

      this.complaintService.updateComplaint(this.complaint.id, updateData).subscribe({
        next: (response) => {
          this.saving = false;
          this.success = 'Complaint updated successfully!';
          this.isEditing = false;
          this.loadComplaint(citizenId, this.complaint!.id, this.userRole);
        },
        error: (error) => {
          this.saving = false;
          this.error = 'Failed to update complaint. Please try again.';
        }
      });
    }
  }

  private validateForm(): boolean {
    if (!this.editData.title.trim()) {
      this.error = 'Please enter a complaint title';
      return false;
    }

    if (!this.editData.description.trim()) {
      this.error = 'Please enter a complaint description';
      return false;
    }

    if (!this.editData.complaintTypeId) {
      this.error = 'Please select a complaint type';
      return false;
    }

    return true;
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

  getFileIcon(fileName: string): string {
    const extension = fileName.split('.').pop()?.toLowerCase();
    switch (extension) {
      case 'pdf': return 'bi-file-earmark-pdf';
      case 'doc':
      case 'docx': return 'bi-file-earmark-word';
      case 'jpg':
      case 'jpeg':
      case 'png':
      case 'gif': return 'bi-file-earmark-image';
      default: return 'bi-file-earmark';
    }
  }





  onSolutionFileSelect(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.agentData.solutionFile = file;
    }
  }

  updateComplaintStatus(): void {
    if (!this.complaint) return;
    
    this.saving = true;
    this.error = '';
    const agentId = this.authService.getUserId();
    
    if (agentId) {
      this.complaintService.updateComplaintStatus(agentId, this.complaint.id, this.agentData.status, this.agentData.solutionFile).subscribe({
        next: () => {
          this.saving = false;
          this.success = 'Complaint status updated successfully!';
          this.isEditing = false;
          this.loadComplaint(agentId, this.complaint!.id, this.userRole);
        },
        error: (error) => {
          this.saving = false;
          this.error = 'Failed to update complaint status. Please try again.';
        }
      });
    }
  }

  deleteComplaint(): void {
    if (!this.complaint) return;
    
    if (confirm('Are you sure you want to delete this complaint? This action cannot be undone.')) {
      const citizenId = this.authService.getUserId();
      if (citizenId) {
        this.complaintService.deleteComplaint(citizenId, this.complaint.id).subscribe({
          next: () => {
            this.success = 'Complaint deleted successfully!';
            setTimeout(() => {
              this.router.navigate(['/citizen/complaints']);
            }, 1500);
          },
          error: (error) => {
            console.error('Error deleting complaint:', error);
            this.error = 'Failed to delete complaint. It may already be assigned to an agent.';
          }
        });
      }
    }
  }

  downloadDocument(documentId: number, fileName: string): void {
    this.complaintService.downloadDocument(documentId).subscribe({
      next: (blob) => {
        const url = window.URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = url;
        link.download = fileName;
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        window.URL.revokeObjectURL(url);
      },
      error: (error) => {
        console.error('Error downloading document:', error);
        this.error = 'Failed to download document. Please try again.';
      }
    });
  }

  goBack(): void {
    const path = this.userRole === 'Citizen' ? '/citizen/complaints' : '/agent/complaints';
    this.router.navigate([path]);
  }
}