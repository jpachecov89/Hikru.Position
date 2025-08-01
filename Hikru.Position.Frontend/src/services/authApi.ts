import axios from 'axios';

const API_BASE_URL = 'https://hikru-api-cdgga0g0cxdvg0e6.centralus-01.azurewebsites.net/api/auth';

export const login = async (username: string, password: string): Promise<void> => {
  const formData = new FormData();
  formData.append('username', username);
  formData.append('password', password);

  const response = await axios.post(`${API_BASE_URL}/login`, formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
    },
  });
  const token = response.data.token;

  localStorage.setItem('authToken', token);

  axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
};

export const logout = (): void => {
  localStorage.removeItem('authToken');
  delete axios.defaults.headers.common['Authorization'];
};