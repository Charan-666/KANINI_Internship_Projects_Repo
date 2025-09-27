import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterLinkActive, NavigationEnd } from '@angular/router';
import { AuthService } from '../../services/auth';
import { UserService } from '../../services/user';
import { UserRole } from '../../models/user.model';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-navigation',
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './navigation.html',
  styleUrl: './navigation.scss'
})
export class NavigationComponent implements OnInit {
  currentUser: any = null;
  userRole = UserRole;
  userPhoto: string | null = null;
  isLandingPage = false;

  constructor(
    private authService: AuthService,
    private userService: UserService,
    private router: Router
  ) {}

  ngOnInit(): void {
    // Check initial route
    this.isLandingPage = this.router.url === '/';
    
    this.authService.currentUser$.subscribe(user => {
      this.currentUser = user;
      if (user?.id && user?.role === UserRole.Citizen) {
        this.loadUserPhoto(user.id);
      }
    });

    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe((event: NavigationEnd) => {
      this.isLandingPage = event.url === '/';
    });
  }

  loadUserPhoto(citizenId: number): void {
    this.userService.getCitizenProfile(citizenId).subscribe({
      next: (profile) => {
        if (profile?.photo) {
          this.userPhoto = `data:image/jpeg;base64,${profile.photo}`;
        } else {
          this.userPhoto = null;
        }
      },
      error: () => {
        this.userPhoto = null;
      }
    });
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  getRoleBasePath(): string {
    const role = this.authService.getUserRole();
    switch (role) {
      case UserRole.Citizen: return '/citizen';
      case UserRole.Agent: return '/agent';
      case UserRole.Admin: return '/admin';
      default: return '';
    }
  }
}
