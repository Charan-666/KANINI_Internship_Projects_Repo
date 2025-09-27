import { Routes } from '@angular/router';
import { LandingComponent } from './components/landing/landing';
import { LoginComponent } from './components/login/login';
import { RegisterComponent } from './components/register/register';
import { DashboardComponent } from './components/dashboard/dashboard';
import { CitizenDashboardComponent } from './components/citizen-dashboard/citizen-dashboard';
import { AgentDashboardComponent } from './components/agent-dashboard/agent-dashboard';
import { AdminDashboardComponent } from './components/admin-dashboard/admin-dashboard';
import { ComplaintListComponent } from './components/complaint-list/complaint-list';
import { CreateComplaintComponent } from './components/create-complaint/create-complaint';
import { ComplaintDetailComponent } from './components/complaint-detail/complaint-detail';
import { ProfileComponent } from './components/profile/profile';
import { UnauthorizedComponent } from './components/unauthorized/unauthorized';
import { authGuard } from './guards/auth-guard';
import { roleGuard } from './guards/role-guard';
import { UserRole } from './models/user.model';

export const routes: Routes = [
  { path: '', component: LandingComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'unauthorized', component: UnauthorizedComponent },
  
  // Protected routes
  { 
    path: 'dashboard', 
    component: DashboardComponent, 
    canActivate: [authGuard] 
  },
  
  // Citizen routes
  { 
    path: 'citizen', 
    canActivate: [authGuard, roleGuard],
    data: { roles: [UserRole.Citizen] },
    children: [
      { path: 'dashboard', component: CitizenDashboardComponent },
      { path: 'profile', component: ProfileComponent },
      { path: 'complaints', component: ComplaintListComponent },
      { path: 'complaints/:id', component: ComplaintDetailComponent },
      { path: 'create-complaint', component: CreateComplaintComponent },
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
    ]
  },
  
  // Agent routes
  { 
    path: 'agent', 
    canActivate: [authGuard, roleGuard],
    data: { roles: [UserRole.Agent] },
    children: [
      { path: 'dashboard', component: AgentDashboardComponent },
      { path: 'profile', component: ProfileComponent },
      { path: 'complaints', component: ComplaintListComponent },
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
    ]
  },
  
  // Admin routes
  { 
    path: 'admin', 
    canActivate: [authGuard, roleGuard],
    data: { roles: [UserRole.Admin] },
    children: [
      { path: 'dashboard', component: AdminDashboardComponent },
      { path: 'profile', component: ProfileComponent },
      { path: 'complaints', component: ComplaintListComponent },
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
    ]
  },
  
  { path: '**', redirectTo: '/login' }
];
