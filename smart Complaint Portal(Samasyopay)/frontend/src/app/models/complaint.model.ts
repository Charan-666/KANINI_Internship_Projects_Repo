export interface Complaint {
  id: number;
  title: string;
  description: string;
  complaintTypeId: number;
  status: number;
  citizenId: number;
  agentId?: number;
  createdAt: Date;
  updatedAt: Date;
  complaintType?: ComplaintType;
  complaintDocuments?: ComplaintDocument[];
}

export enum ComplaintStatus {
  Pending = 'Pending',
  Assigned = 'Assigned',
  InProgress = 'InProgress',
  Resolved = 'Resolved',
  Rejected = 'Rejected'
}

export interface ComplaintType {
  id: number;
  name: string;
  typeName: string;
}

export interface ComplaintDocument {
  id: number;
  complaintId: number;
  fileName: string;
  contentType: string;
  type: DocumentType;
  uploadedAt: Date;
}

export enum DocumentType {
  Uploaded = 'Uploaded',
  Solved = 'Solved'
}

export interface CreateComplaintRequest {
  title: string;
  description: string;
  complaintTypeId: number;
  documents?: File[];
}