export interface CreatePositionDto {
  title: string;
  description: string;
  location: string;
  status: number;
  recruiterId: string;
  departmentId: string;
  budget: number;
  closingDate: string | null;
}