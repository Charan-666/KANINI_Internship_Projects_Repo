import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth';

@Component({
  selector: 'app-landing',
  imports: [CommonModule, FormsModule],
  templateUrl: './landing.html',
  styleUrl: './landing.scss'
})
export class LandingComponent implements OnInit, OnDestroy {
  showLoginDialog = true;
  currentSlide = 0;
  carouselInterval: any;
  
  loginData = {
    email: '',
    password: ''
  };

  carouselImages = [
    { src: 'aadharcaur.jpg', title: 'Aadhaar Services', desc: 'Quick and secure Aadhaar related services' },
    { src: 'birthcaur.png', title: 'Birth Certificate', desc: 'Easy birth certificate registration and updates' },
    { src: 'pancaur.webp', title: 'PAN Services', desc: 'Comprehensive PAN card services' },
    { src: 'voterIDcaur.jpeg', title: 'Voter ID Services', desc: 'Voter registration and ID services' }
  ];

  complaintCategories = [
    { title: 'Aadhaar Issues', desc: 'Address correction, mobile update, demographic changes', icon: '🆔', color: 'from-blue-500 to-purple-600' },
    { title: 'Birth Certificate', desc: 'New registration, corrections, duplicate certificates', icon: '👶', color: 'from-green-500 to-teal-600' },
    { title: 'PAN Card Services', desc: 'New PAN, corrections, name changes', icon: '💳', color: 'from-orange-500 to-red-600' },
    { title: 'Voter ID Services', desc: 'New registration, address change, photo update', icon: '🗳️', color: 'from-purple-500 to-pink-600' },
    { title: 'Ration Card', desc: 'New ration card, corrections, family member updates', icon: '🍚', color: 'from-yellow-500 to-orange-600' },
    { title: 'Property Tax', desc: 'Tax payment, assessment, property registration', icon: '🏠', color: 'from-indigo-500 to-blue-600' }
  ];

  governmentSchemes = [
    'Aadhaar Services – Enrollment, update (address, mobile, biometric), e-Aadhaar download',
    'PAN Card Services – New PAN, corrections, reprint, linking with Aadhaar',
    'Voter ID Services – New registration, address change, corrections, EPIC download',
    'Birth Certificate Services – New registration, corrections, duplicate copies',
    'Ration Card Services – New ration card, correction, member addition/removal',
    'Property Tax & Khata Services – Assessment, payment, transfer'
  ];

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {
    this.startCarousel();
  }

  ngOnDestroy() {
    if (this.carouselInterval) {
      clearInterval(this.carouselInterval);
    }
  }

  startCarousel() {
    this.carouselInterval = setInterval(() => {
      this.nextSlide();
    }, 8000);
  }

  nextSlide() {
    this.currentSlide = (this.currentSlide + 1) % this.carouselImages.length;
  }

  prevSlide() {
    this.currentSlide = this.currentSlide === 0 ? this.carouselImages.length - 1 : this.currentSlide - 1;
  }

  goToSlide(index: number) {
    this.currentSlide = index;
  }

  closeLoginDialog() {
    this.showLoginDialog = false;
  }

  onLogin() {
    this.authService.login(this.loginData).subscribe({
      next: (response) => {
        this.closeLoginDialog();
        const role = this.authService.getUserRole();
        switch (role) {
          case 'Citizen': this.router.navigate(['/citizen/dashboard']); break;
          case 'Agent': this.router.navigate(['/agent/dashboard']); break;
          case 'Admin': this.router.navigate(['/admin/dashboard']); break;
          default: this.router.navigate(['/dashboard']);
        }
      },
      error: (error) => {
        console.error('Login failed:', error);
      }
    });
  }

  navigateToRegister() {
    this.router.navigate(['/register']);
  }
}