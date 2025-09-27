import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ComplaintService } from '../../services/complaint';
import { AuthService } from '../../services/auth';
import { CreateComplaintRequest, ComplaintType } from '../../models/complaint.model';

@Component({
  selector: 'app-create-complaint',
  imports: [CommonModule, FormsModule],
  templateUrl: './create-complaint.html',
  styleUrl: './create-complaint.scss'
})
export class CreateComplaintComponent implements OnInit {
  complaintData: CreateComplaintRequest = {
    title: '',
    description: '',
    complaintTypeId: 0
  };
  
  complaintTypes: ComplaintType[] = [];
  selectedFiles: File[] = [];
  loading = false;
  error = '';
  success = '';

  constructor(
    private complaintService: ComplaintService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadComplaintTypes();
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

  onFileSelect(event: any): void {
    const files = Array.from(event.target.files) as File[];
    this.selectedFiles = files;
    this.complaintData.documents = files;
  }

  removeFile(index: number): void {
    this.selectedFiles.splice(index, 1);
    this.complaintData.documents = this.selectedFiles;
  }

  onSubmit(): void {
    if (!this.validateForm()) {
      return;
    }

    this.loading = true;
    this.error = '';
    const citizenId = this.authService.getUserId();

    if (citizenId) {
      this.complaintService.createComplaint(citizenId, this.complaintData).subscribe({
        next: (response) => {
          this.loading = false;
          this.success = 'Complaint submitted successfully!';
          setTimeout(() => {
            this.router.navigate(['/citizen/complaints']);
          }, 2000);
        },
        error: (error) => {
          this.loading = false;
          this.error = 'Failed to submit complaint. Please try again.';
        }
      });
    }
  }

  private validateForm(): boolean {
    if (!this.complaintData.title.trim()) {
      this.error = 'Please enter a complaint title';
      return false;
    }

    if (!this.complaintData.description.trim()) {
      this.error = 'Please enter a complaint description';
      return false;
    }

    if (!this.complaintData.complaintTypeId) {
      this.error = 'Please select a complaint type';
      return false;
    }

    return true;
  }

  goBack(): void {
    this.router.navigate(['/citizen/dashboard']);
  }
}
