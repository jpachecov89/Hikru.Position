export interface Position {
  positionId: string;
  title: string;
  description: string;
  location: string;
  status: number;
  recruiterId: string;
  recruiter: string;
  departmentId: string;
  department: string;
  budget: number;
  closingDate?: string;
}