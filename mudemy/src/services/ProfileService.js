import axios from 'axios';
import { getToken } from '../helpers/AuthHelper';

const API_URL = 'https://localhost:5001/api/User/';

const ProfileService = {
  async getProfile() {
    const token = getToken(); 
    console.log('Token:', token);
    try {
      const response = await axios.get(`${API_URL}Profile`, {
        headers: { 'Authorization': `Bearer ${token}` },
        withCredentials: true,
      });
      console.log('Response Data:', response.data); 
      console.log('Response Data data:', response.data.data); 
      return response.data.data;
    } catch (error) {
      throw new Error('An error occurred while retrieving user profile information!');
    }
  },
};

export default ProfileService;
