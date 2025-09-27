export interface User {
  id: number;
  name: string;
  email: string;
  role: UserRole;
  photo?: string;
}

export enum UserRole {
  Citizen = 'Citizen',
  Agent = 'Agent',
  Admin = 'Admin'
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  role: string;
  userId: number;
  userName: string;
}

export interface RegisterRequest {
  name: string;
  email: string;
  password: string;
  address?: string;
  phoneNumber?: string;
  photo?: File;
}