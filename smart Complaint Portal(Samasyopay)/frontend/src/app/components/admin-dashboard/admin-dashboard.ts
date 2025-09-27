import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AdminService } from '../../services/admin';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.html',
  styleUrls: ['./admin-dashboard.scss'],
  standalone: true,
  imports: [CommonModule, FormsModule]
})
export class AdminDashboardComponent implements OnInit {
  activeTab = 'dashboard';
  
  // Users
  users: any[] = [];
  agents: any[] = [];
  citizens: any[] = [];
  userTypeFilter = 'agents'; // Default to agents
  
  // Complaints
  complaints: any[] = [];
  complaintTypes: any[] = [];
  
  // Loading states
  isLoadingUsers = false;
  isLoadingComplaints = false;
  isLoadingComplaintTypes = false;
  
  // Filters
  statusFilter = '';
  typeFilter = '';
  searchTerm = '';
  filteredComplaints: any[] = [];
  
  // Forms
  newAgent = { name: '', email: '', password: '', department: '' };
  editAgent: any = null;
  newComplaintType = { name: '', typeName: '' };
  editComplaintType: any = null;
  searchFilters = {};
  
  constructor(private adminService: AdminService, private route: ActivatedRoute) {}
  
  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      if (params['tab']) {
        this.activeTab = params['tab'];
      } else {
        this.activeTab = 'dashboard';
      }
    });
    this.loadUsers();
    this.loadComplaints();
    this.loadComplaintTypes();
  }
  
  setActiveTab(tab: string) {
    this.activeTab = tab;
  }
  
  // User Management
  loadUsers() {
    this.isLoadingUsers = true;
    this.users = [];
    this.agents = [];
    
    this.adminService.getAllUsers().subscribe({
      next: (users: any) => {
        console.log('Loaded users:', users);
        this.users = Array.isArray(users) ? users : [];
        this.agents = this.users.filter(u => u?.role === 'Agent' || u?.role === 1);
        this.citizens = this.users.filter(u => u?.role === 'Citizen' || u?.role === 0);
        console.log('Filtered agents:', this.agents);
        console.log('Filtered citizens:', this.citizens);
        this.isLoadingUsers = false;
      },
      error: (error: any) => {
        console.error('Error loading users:', error);
        this.users = [];
        this.agents = [];
        this.citizens = [];
        this.isLoadingUsers = false;
      }
    });
  }
  
  addAgent() {
    console.log('Adding agent:', this.newAgent);
    const agentData = {
      Name: this.newAgent.name,
      Email: this.newAgent.email,
      Password: this.newAgent.password,
      Department: this.newAgent.department
    };
    
    this.adminService.addAgent(agentData).subscribe({
      next: (response: any) => {
        console.log('Agent added successfully:', response);
        this.loadUsers();
        this.newAgent = { name: '', email: '', password: '', department: '' };
      },
      error: (error: any) => {
        console.error('Error adding agent:', error);
        alert('Error adding agent: ' + (error.error?.message || error.message));
      }
    });
  }
  
  editAgentData(agent: any) {
    this.editAgent = {
      id: agent.id,
      name: agent.name,
      email: agent.email,
      password: '',
      department: agent.agentProfile?.department
    };
  }

  updateAgent() {
    const agentData = {
      Name: this.editAgent.name,
      Email: this.editAgent.email,
      Password: this.editAgent.password,
      Department: this.editAgent.department
    };
    
    this.adminService.updateAgent(this.editAgent.id, agentData).subscribe({
      next: (response: any) => {
        console.log('Agent updated successfully:', response);
        this.editAgent = null;
        // Force refresh with a small delay
        setTimeout(() => {
          this.loadUsers();
        }, 100);
      },
      error: (error: any) => {
        console.error('Error updating agent:', error);
        alert('Error updating agent: ' + (error.error?.message || error.message));
      }
    });
  }

  cancelEdit() {
    this.editAgent = null;
  }

  deleteAgent(agentId: number) {
    if (confirm('Are you sure you want to delete this agent?')) {
      this.adminService.deleteAgent(agentId).subscribe(() => {
        this.loadUsers();
      });
    }
  }
  
  // Complaint Management
  loadComplaints() {
    this.isLoadingComplaints = true;
    this.complaints = [];
    
    this.adminService.getAllComplaints().subscribe({
      next: (complaints: any) => {
        console.log('Loaded complaints:', complaints);
        this.complaints = Array.isArray(complaints) ? complaints : [];
        this.applyFilters();
        this.isLoadingComplaints = false;
      },
      error: (error: any) => {
        console.error('Error loading complaints:', error);
        this.complaints = [];
        this.filteredComplaints = [];
        this.isLoadingComplaints = false;
      }
    });
  }
  
  applyFilters() {
    this.filteredComplaints = this.complaints.filter(complaint => {
      const matchesStatus = !this.statusFilter || complaint?.status?.toString() === this.statusFilter;
      const matchesType = !this.typeFilter || complaint?.complaintType?.id?.toString() === this.typeFilter;
      const matchesSearch = !this.searchTerm || 
        complaint?.title?.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        complaint?.citizen?.user?.email?.toLowerCase().includes(this.searchTerm.toLowerCase());
      
      return matchesStatus && matchesType && matchesSearch;
    });
  }
  
  onFilterChange() {
    this.applyFilters();
  }
  
  clearFilters() {
    this.statusFilter = '';
    this.typeFilter = '';
    this.searchTerm = '';
    this.applyFilters();
  }
  
  getComplaintStats() {
    const total = this.complaints.length;
    const pending = this.complaints.filter(c => c?.status === 0).length;
    const assigned = this.complaints.filter(c => c?.status === 1).length;
    const resolved = this.complaints.filter(c => c?.status === 3).length;
    
    return { total, pending, assigned, resolved };
  }
  
  assignComplaint(complaintId: number, agentId: number) {
    this.adminService.assignComplaint(complaintId, agentId).subscribe({
      next: (response: any) => {
        console.log('Complaint assigned:', response);
        this.loadComplaints();
      },
      error: (error: any) => {
        console.error('Error assigning complaint:', error);
      }
    });
  }

  onAssignAgent(event: Event, complaintId: number) {
    const target = event.target as HTMLSelectElement;
    const agentId = parseInt(target.value);
    if (agentId && complaintId) {
      this.assignComplaint(complaintId, agentId);
    }
  }
  
  // Complaint Type Management
  loadComplaintTypes() {
    this.isLoadingComplaintTypes = true;
    this.complaintTypes = [];
    
    this.adminService.getAllComplaintTypes().subscribe({
      next: (types: any) => {
        this.complaintTypes = Array.isArray(types) ? types : [];
        this.isLoadingComplaintTypes = false;
      },
      error: (error: any) => {
        console.error('Error loading complaint types:', error);
        this.complaintTypes = [];
        this.isLoadingComplaintTypes = false;
      }
    });
  }
  
  addComplaintType() {
    this.adminService.createComplaintType(this.newComplaintType).subscribe(() => {
      this.loadComplaintTypes();
      this.newComplaintType = { name: '', typeName: '' };
    });
  }

  editComplaintTypeData(type: any) {
    this.editComplaintType = {
      id: type.id,
      name: type.name,
      typeName: type.typeName
    };
  }

  updateComplaintType() {
    this.adminService.updateComplaintType(this.editComplaintType.id, this.editComplaintType).subscribe(() => {
      this.loadComplaintTypes();
      this.editComplaintType = null;
    });
  }

  cancelComplaintTypeEdit() {
    this.editComplaintType = null;
  }
  
  deleteComplaintType(id: number) {
    if (confirm('Are you sure you want to delete this complaint type?')) {
      this.adminService.deleteComplaintType(id).subscribe(() => {
        this.loadComplaintTypes();
      });
    }
  }

  // Helper methods for status display
  getStatusText(status: number): string {
    console.log('Status value received:', status, typeof status);
    const statusMap: { [key: number]: string } = {
      0: 'Pending',
      1: 'Assigned', 
      2: 'InProgress',
      3: 'Resolved'
    };
    return statusMap[status] || `Unknown (${status})`;
  }

  getStatusClass(status: number): string {
    const classMap: { [key: number]: string } = {
      0: 'secondary',
      1: 'primary',
      2: 'warning', 
      3: 'success'
    };
    return classMap[status] || 'secondary';
  }

  getAgentName(complaint: any): string {
    if (complaint.agent?.user?.name) return complaint.agent.user.name;
    if (complaint.agentId) {
      const agent = this.agents.find(a => a.id === complaint.agentId);
      return agent?.name || 'Agent ID: ' + complaint.agentId;
    }
    return 'Unassigned';
  }

  getCitizenEmail(complaint: any): string {
    if (complaint?.citizen?.user?.email) return complaint.citizen.user.email;
    if (complaint?.citizenId) {
      const citizen = this.users.find(u => u?.id === complaint.citizenId && (u?.role === 'Citizen' || u?.role === 0));
      return citizen?.email || 'Citizen ID: ' + complaint.citizenId;
    }
    return 'No Citizen';
  }
  
  trackByComplaintId(index: number, complaint: any): any {
    return complaint?.id || index;
  }

  onUserTypeFilterChange() {
    // Filter change handled by template
  }

  getDisplayedUsers() {
    return this.userTypeFilter === 'agents' ? this.agents : this.citizens;
  }

  deleteCitizen(citizenId: number) {
    if (confirm('Are you sure you want to delete this citizen?')) {
      this.adminService.deleteUser(citizenId).subscribe(() => {
        this.loadUsers();
      });
    }
  }
}