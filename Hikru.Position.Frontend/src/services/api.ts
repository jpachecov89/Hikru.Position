import axios from 'axios';
import { Position } from '../types/position';
import { CreatePositionDto } from '../types/createPosition';
import { UpdatePositionDto } from '../types/updatePosition';
import { Recruiter } from '../types/recruiter';
import { Department } from '../types/department';

const API_BASE_URL = 'https://hikru-api-cdgga0g0cxdvg0e6.centralus-01.azurewebsites.net';

export const getPositions = async (): Promise<Position[]> => {
  const response = await axios.get(`${API_BASE_URL}/api/positions`);
  return response.data;
};

export const getPositionById = async (id: string): Promise<Position> => {
  const response = await axios.get(`${API_BASE_URL}/api/positions/${id}`);
  return response.data;
};

export const createPosition = async (position: CreatePositionDto): Promise<Position> => {
  const response = await axios.post(`${API_BASE_URL}/api/positions`, position);
  return response.data;
};

export const updatePosition = async (id: string, position: UpdatePositionDto): Promise<Position> => {
  const response = await axios.put(`${API_BASE_URL}/api/positions/${id}`, position);
  return response.data;
};

export const deletePosition = async (id: string): Promise<void> => {
  await axios.delete(`${API_BASE_URL}/api/positions/${id}`);
};

export const getRecruiters = async (): Promise<Recruiter[]> => {
  const response = await axios.get(`${API_BASE_URL}/api/recruiters`);
  return response.data;
};

export const getDepartments = async (): Promise<Department[]> => {
  const response = await axios.get(`${API_BASE_URL}/api/departments`);
  return response.data;
};