import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UserService } from '../../services/user';
import { AuthService } from '../../services/auth';

@Component({
  selector: 'app-profile',
  imports: [CommonModule, FormsModule],
  templateUrl: './profile.html',
  styleUrl: './profile.scss'
})
export class ProfileComponent implements OnInit {
  profile: any = null;
  loading = true;
  saving = false;
  error = '';
  success = '';
  selectedPhoto: File | null = null;

  profileData = {
    address: '',
    phoneNumber: ''
  };

  constructor(
    private userService: UserService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.loadProfile();
  }

  loadProfile(): void {
    this.loading = true;
    const citizenId = this.authService.getUserId();
    
    if (citizenId) {
      this.userService.getCitizenProfile(citizenId).subscribe({
        next: (profile) => {
          this.profile = profile;
          this.profileData = {
            address: profile.address || '',
            phoneNumber: profile.phoneNumber || ''
          };
          this.loading = false;
        },
        error: (error) => {
          console.error('Error loading profile:', error);
          this.loading = false;
        }
      });
    }
  }

  onPhotoSelect(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.selectedPhoto = file;
    }
  }

  onSubmit(): void {
    this.saving = true;
    this.error = '';
    const citizenId = this.authService.getUserId();

    if (citizenId) {
      const updateData = {
        ...this.profileData,
        photo: this.selectedPhoto
      };

      this.userService.updateCitizenProfile(citizenId, updateData).subscribe({
        next: (response) => {
          this.saving = false;
          this.success = 'Profile updated successfully!';
          this.selectedPhoto = null;
          this.loadProfile();
          // Refresh current user to update navbar photo
          this.authService.setCurrentUser();
        },
        error: (error) => {
          this.saving = false;
          this.error = 'Failed to update profile. Please try again.';
        }
      });
    }
  }
}
